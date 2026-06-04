using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Core.Dtos.TaskModelDtos;
using Assignment.Core.Enums;

namespace Assignment.Core.ServiceInterfaces
{
    public interface ITaskService
    {
        Task<int> CreateTask(TaskModelDto taskDto, string userId);
        Task<TaskModelReturnDto> GetTaskById(int taskId, string userId);
        Task<List<TaskModelReturnDto>> GetAllTasks(string userId);
        Task<bool> UpdateTaskData(TaskModelUpdateDto taskdto, string userId);
    }
}
