using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Core.Entities;
using Assignment.Core.Enums;

namespace Assignment.Core.RepoInterfaces
{
    public interface ITaskModelRepository
    {
        Task<TaskModel> GetUserTaskWithTheSameTitle(string title, string userId);
        Task CreateTask(TaskModel task);
        Task<TaskModel> GetTaskById(int taskId, string userId);
        Task<List<TaskModel>> GetAllTasks(string userId);
        void Update(TaskModel task);
        Task<int> SaveChanges();
        Task UpdateTaskStatus(int taskId, Status status);
    }
}
