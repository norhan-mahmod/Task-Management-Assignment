using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Assignment.Core.Dtos.TaskModelDtos;
using Assignment.Core.Entities;
using Assignment.Core.Enums;
using Assignment.Core.RepoInterfaces;
using Assignment.Core.ServiceInterfaces;
using Assignment.Service.ExceptionHandling;
using AutoMapper;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Assignment.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskModelRepository taskModelRepository;
        private readonly IMapper mapper;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly ICachService cachService;

        public TaskService(ITaskModelRepository taskModelRepository , IMapper mapper, 
                           IBackgroundJobClient backgroundJobClient, ICachService cachService)
        {
            this.taskModelRepository = taskModelRepository;
            this.mapper = mapper;
            this.backgroundJobClient = backgroundJobClient;
            this.cachService = cachService;
        }
        public async Task<int> CreateTask(TaskModelDto taskDto , string userId)
        {
            //Prevent creating duplicate tasks with the same title on the same day for the same user 
            var SimilarTask = await taskModelRepository.GetUserTaskWithTheSameTitle(taskDto.Title, userId);
            if (SimilarTask is not null)
                throw new BadRequestException("You Already Have Task With same title Today");

            //Save Task To Database
            var task = mapper.Map<TaskModel>(taskDto);
            task.Status = Status.Pending;
            task.CreatedAt = DateTime.UtcNow;
            task.UserId = userId;
            await taskModelRepository.CreateTask(task);
            await taskModelRepository.SaveChanges();

            //Queue task For Background Processing using Hangfire
            backgroundJobClient.Enqueue<ITaskProcessor>(t => t.ProcessTaskAsync(task.Id));

            return task.Id;
        }

        public async Task<List<TaskModelReturnDto>> GetAllTasks(string userId)
        {
            var tasks = await taskModelRepository.GetAllTasks(userId);
            var result = mapper.Map<List<TaskModelReturnDto>>(tasks);
            return result;
        }

        public async Task<TaskModelReturnDto> GetTaskById(int taskId , string userId)
        {
            //From Redis
            var cachedTask = await cachService.Get($"task:{taskId}");
            if (!string.IsNullOrEmpty(cachedTask))
                return JsonSerializer.Deserialize<TaskModelReturnDto>(cachedTask);

            // From Database 
            var task = await taskModelRepository.GetTaskById(taskId , userId);
            if (task is null)
                throw new NotFoundException("Task is Not Found");

            var taskDto = mapper.Map<TaskModelReturnDto>(task);
            //cach task in redis
            await cachService.Set($"task:{taskId}", JsonSerializer.Serialize(taskDto));

            return taskDto;
        }

        public async Task<bool> UpdateTaskData(TaskModelUpdateDto taskdto , string userId)
        {
            var task = await taskModelRepository.GetTaskById(taskdto.Id , userId);
            if (task is null)
                throw new NotFoundException("Task is Not Found");

            mapper.Map(taskdto ,task);
            taskModelRepository.Update(task);
            await taskModelRepository.SaveChanges();

            var taskReturnDto = mapper.Map<TaskModelReturnDto>(task);
            //Refresh Cached task
            await cachService.Set($"task:{task.Id}", JsonSerializer.Serialize(taskReturnDto));
            return true;
        }

    }
}
