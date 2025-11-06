
using TaskManagement.Application.DTOs.Tasks;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Interfaces.IServices
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<TaskDto> GetTaskByIdAsync(Guid id);
        Task<IEnumerable<TaskDto>> GetTasksByProjectIdAsync(Guid projectId);
        Task<IEnumerable<TaskDto>> GetTasksAssignedToUserAsync(Guid userId);
        Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto, Guid createdById);
        Task<TaskDto> UpdateTaskAsync(Guid id, UpdateTaskDto updateTaskDto);
        Task<TaskDto> UpdateTaskStatusAsync(Guid id, TaskEntityStatus status);
        Task DeleteTaskAsync(Guid id);
    }
}
