
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces.IRepositories
{
    public interface IProjectRepository
    {
        Task<Project?> GetByIdAsync(Guid id);
        Task<IEnumerable<Project>> GetAllAsync();
        Task<IEnumerable<Project>> GetByOwnerIdAsync(Guid ownerId);
        Task<Project> CreateAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
