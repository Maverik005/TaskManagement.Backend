using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs.Tasks;
using TaskManagement.Application.Interfaces.IServices;
using TaskManagement.Domain.Enums;
using TaskManagement.Domain.Exceptions;

namespace TaskManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ITaskService taskService, ILogger<TasksController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            try
            {
                var task = await _taskService.GetTaskByIdAsync(id);
                return Ok(task);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("assigned-to-me")]
        [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyTasks()
        {
            var userId = GetCurrentUserId();
            var tasks = await _taskService.GetTasksAssignedToUserAsync(userId);
            return Ok(tasks);
        }

        [HttpGet("project/{projectId}")]
        [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTasksByProject(Guid projectId)
        {
            var tasks = await _taskService.GetTasksByProjectIdAsync(projectId);
            return Ok(tasks);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TaskDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
        {
            try
            {
                var userId = GetCurrentUserId();
                var task = await _taskService.CreateTaskAsync(createTaskDto, userId);
                return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskDto updateTaskDto)
        {
            try
            {
                var task = await _taskService.UpdateTaskAsync(id, updateTaskDto);
                return Ok(task);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/status")]
        [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTaskStatus(Guid id, [FromBody] TaskStatus status)
        {
            try
            {
                var task = await _taskService.UpdateTaskStatusAsync(id, status);
                return Ok(task);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            try
            {
                await _taskService.DeleteTaskAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userIdClaim!);
        }
    }
}
