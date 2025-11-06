using AutoMapper;
using TaskManagement.Application.DTOs.Projects;
using TaskManagement.Application.Interfaces.IRepositories;
using TaskManagement.Application.Interfaces.IServices;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Exceptions;

namespace TaskManagement.Application.Services
{
    public class ProjectService: IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProjectService(
            IProjectRepository projectRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> GetProjectByIdAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project == null)
            {
                throw new NotFoundException(nameof(Project), id);
            }

            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<IEnumerable<ProjectDto>> GetProjectsByOwnerAsync(Guid ownerId)
        {
            var projects = await _projectRepository.GetByOwnerIdAsync(ownerId);
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto createProjectDto, Guid ownerId)
        {
            // Verify owner exists
            if (!await _userRepository.ExistsAsync(ownerId))
            {
                throw new NotFoundException(nameof(User), ownerId);
            }

            var project = _mapper.Map<Project>(createProjectDto);
            project.Id = Guid.NewGuid();
            project.OwnerId = ownerId;

            var createdProject = await _projectRepository.CreateAsync(project);
            return _mapper.Map<ProjectDto>(createdProject);
        }

        public async Task<ProjectDto> UpdateProjectAsync(Guid id, UpdateProjectDto updateProjectDto)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project == null)
            {
                throw new NotFoundException(nameof(Project), id);
            }

            _mapper.Map(updateProjectDto, project);
            await _projectRepository.UpdateAsync(project);

            var updatedProject = await _projectRepository.GetByIdAsync(id);
            return _mapper.Map<ProjectDto>(updatedProject);
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            if (!await _projectRepository.ExistsAsync(id))
            {
                throw new NotFoundException(nameof(Project), id);
            }

            await _projectRepository.DeleteAsync(id);
        }
    }
}
