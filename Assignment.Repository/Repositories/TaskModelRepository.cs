using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Core.Dtos.TaskModelDtos;
using Assignment.Core.Entities;
using Assignment.Core.Enums;
using Assignment.Core.RepoInterfaces;
using Assignment.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repository.Repositories
{
    public class TaskModelRepository : ITaskModelRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TaskModelRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateTask(TaskModel task)
            => await dbContext.TaskModel.AddAsync(task);

        public async Task<TaskModel> GetUserTaskWithTheSameTitle(string title, string userId)
            => await dbContext.TaskModel.FirstOrDefaultAsync(t => t.UserId == userId &&
                                                    t.Title.ToLower() == title.ToLower() &&
                                                    t.CreatedAt.Date == DateTime.UtcNow.Date
                                );

        public async Task<List<TaskModel>> GetAllTasks(string userId)
            => await dbContext.TaskModel.Where(t => t.UserId == userId)
                                        .OrderBy(t => t.Priority).ThenBy(t => t.CreatedAt).ToListAsync();

        public async Task<TaskModel> GetTaskById(int taskId, string userId)
            => await dbContext.TaskModel.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

        public void Update(TaskModel task)
            => dbContext.TaskModel.Update(task);

        public async Task<int> SaveChanges()
            => await dbContext.SaveChangesAsync();


        public async Task UpdateTaskStatus(int taskId, Status status)
        {
            var task = await dbContext.TaskModel.FindAsync(taskId);
            if (task is null)
                return;

            task.Status = status;
            Update(task);
            await SaveChanges();
        }

    }
}
