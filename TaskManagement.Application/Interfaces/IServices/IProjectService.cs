
using TaskManagement.Application.DTOs.Projects;

namespace TaskManagement.Application.Interfaces.IServices
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto> GetProjectByIdAsync(Guid id);
        Task<IEnumerable<ProjectDto>> GetProjectsByOwnerAsync(Guid ownerId);
        Task<ProjectDto> CreateProjectAsync(CreateProjectDto createProjectDto, Guid ownerId);
        Task<ProjectDto> UpdateProjectAsync(Guid id, UpdateProjectDto updateProjectDto);
        Task DeleteProjectAsync(Guid id);
    }
}
