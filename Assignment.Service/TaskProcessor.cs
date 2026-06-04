using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Core.Enums;
using Assignment.Core.RepoInterfaces;
using Assignment.Core.ServiceInterfaces;
using Assignment.Service.ExceptionHandling;

namespace Assignment.Service
{
    public class TaskProcessor : ITaskProcessor
    {
        private readonly ITaskModelRepository taskModelRepository;
        private readonly ICachService cachService;

        public TaskProcessor(ITaskModelRepository taskModelRepository , ICachService cachService)
        {
            this.taskModelRepository = taskModelRepository;
            this.cachService = cachService;
        }

        public async Task ProcessTaskAsync(int taskId)
        {
            //Change status to InProgress
            await taskModelRepository.UpdateTaskStatus(taskId, Status.InProgress);
            await cachService.Delete($"task:{taskId}"); // Udate Cach Redis

            //Simulate Processing
            await Task.Delay(TimeSpan.FromMinutes(1));

            //Change status to Done
            await taskModelRepository.UpdateTaskStatus(taskId, Status.Done);
            await cachService.Delete($"task:{taskId}");// Udate Cach Redis

        }

    }
}
