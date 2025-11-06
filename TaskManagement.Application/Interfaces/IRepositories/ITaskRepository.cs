using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Interfaces.IRepositories
{
    public interface ITaskRepository
    {
        Task<TaskEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskEntity>> GetAllAsync();
        Task<IEnumerable<TaskEntity>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<TaskEntity>> GetByAssignedToIdAsync(Guid userId);
        Task<IEnumerable<TaskEntity>> GetByStatusAsync(TaskEntityStatus status);
        Task<TaskEntity> CreateAsync(TaskEntity task);
        Task UpdateAsync(TaskEntity task);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
