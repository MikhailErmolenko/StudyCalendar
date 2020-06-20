using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCalendar.Common.Contracts;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.Task;
using WebCalendar.Services.Models.User;
using WebCalendar.WebApi.Models.Task;

namespace WebCalendar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService, IMapper mapper, IUserService userService)
        {
            _taskService = taskService;
            _mapper = mapper;
            _userService = userService;
        }
        
        [HttpPost("{userId}")]
        public async Task<ActionResult<TaskModel>> CreateTask(Guid userId, [FromBody] TaskCreationModel taskCreationModel)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user == null || user.Id != userId)
            {
                return Unauthorized();
            }
            
            TaskCreationServiceModel taskCreationServiceModel = _mapper
                .Map<TaskCreationModel, TaskCreationServiceModel>(taskCreationModel);
            TaskServiceModel taskServiceModel = await _taskService.AddAsync(taskCreationServiceModel);

            TaskModel taskModel = _mapper.Map<TaskServiceModel, TaskModel>(taskServiceModel);

            return CreatedAtAction(nameof(GetTask), new
            {
                taskId = taskModel.Id,
                userId = user.Id
            }, taskModel);
        }

        [HttpGet("{taskId}/{userId}")]
        public async Task<ActionResult<TaskModel>> GetTask(Guid taskId, Guid userId)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user == null || user.Id != userId)
            {
                return Unauthorized();
            }

            TaskServiceModel taskServiceModel = await _taskService.GetByIdAsync(taskId);
            
            TaskModel taskModel = _mapper.Map<TaskServiceModel, TaskModel>(taskServiceModel);

            return Ok(taskModel);
        }

        // GET: api/task/{calendarId}/{userId}
        [HttpGet("all/{calendarId}/{userId}")]
        public async Task<ActionResult<TaskModel>> GetAllTasks(Guid calendarId, Guid userId)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user == null || user.Id != userId)
            {
                return Unauthorized();
            }

            IEnumerable<TaskServiceModel> taskServiceModels = await _taskService.GetAllByCalendarIdAsync(calendarId);

            IEnumerable<TaskModel> taskModels = _mapper
                .Map<IEnumerable<TaskServiceModel>, IEnumerable<TaskModel>>(taskServiceModels);

            return Ok(taskModels);
        }

        // PUT: api/task/completion/{taskId}/{userId}
        [HttpPut("completion/{taskId}/{userId}")]
        public async Task<IActionResult> SetTaskCompletion(Guid taskId, Guid userId,
            [FromBody] TaskCompletionModel task)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user == null || user.Id != userId)
            {
                return Unauthorized();
            }

            if (taskId != task.Id)
            {
                return BadRequest();
            }

            TaskServiceModel taskServiceModel = await _taskService.GetByIdAsync(taskId);

            if (taskServiceModel == null)
            {
                return NotFound();
            }

            await _taskService.TaskCompletion(taskId, task.IsDone);

            return NoContent();
        }
        
        // DELETE : api/task/{taskId}/{userId}
        [HttpDelete("{taskId}/{userId}")]
        public async Task<IActionResult> DeleteTask(Guid taskId, Guid userId)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user == null || user.Id != userId)
            {
                return Unauthorized();
            }

            bool exists = await _taskService.ExistsAsync(taskId);

            if (!exists)
            {
                return NotFound();
            }

            await _taskService.RemoveAsync(taskId);

            return Ok();
        }
    }
}