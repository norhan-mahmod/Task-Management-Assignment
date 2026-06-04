using System.Security.Claims;
using Assignment.API.Helper;
using Assignment.Core.Dtos.TaskModelDtos;
using Assignment.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService taskService;

        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask(TaskModelDto taskDto)
        {
            var result = await taskService.CreateTask(taskDto, UserHelper.GetUserId(User));
            return Ok(result);
        }

        [HttpGet("GetTaskById")]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            var result = await taskService.GetTaskById(taskId, UserHelper.GetUserId(User));
            return Ok(result);
        }

        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = await taskService.GetAllTasks(UserHelper.GetUserId(User));
            return Ok(result);
        }

        [HttpPut("UpdateTaskData")]
        public async Task<IActionResult> UpdateTaskData(TaskModelUpdateDto taskdto)
        {
            var result = await taskService.UpdateTaskData(taskdto, UserHelper.GetUserId(User));
            return Ok(result);
        }

    }
}
