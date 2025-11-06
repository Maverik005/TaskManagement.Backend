using AutoMapper;
using TaskManagement.Application.DTOs.Tasks;
using TaskManagement.Application.Interfaces.IRepositories;
using TaskManagement.Application.Interfaces.IServices;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
using TaskManagement.Domain.Exceptions;

namespace TaskManagement.Application.Services
{
    public class TaskService: ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public TaskService(
            ITaskRepository taskRepository,
            IProjectRepository projectRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<TaskDto> GetTaskByIdAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
            {
                throw new NotFoundException(nameof(TaskEntity), id);
            }

            return _mapper.Map<TaskDto>(task);
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByProjectIdAsync(Guid projectId)
        {
            var tasks = await _taskRepository.GetByProjectIdAsync(projectId);
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<IEnumerable<TaskDto>> GetTasksAssignedToUserAsync(Guid userId)
        {
            var tasks = await _taskRepository.GetByAssignedToIdAsync(userId);
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto, Guid createdById)
        {
            // Verify project exists
            if (!await _projectRepository.ExistsAsync(createTaskDto.ProjectId))
            {
                throw new NotFoundException(nameof(Project), createTaskDto.ProjectId);
            }

            // Verify creator exists
            if (!await _userRepository.ExistsAsync(createdById))
            {
                throw new NotFoundException(nameof(User), createdById);
            }

            // Verify assigned user exists (if provided)
            if (createTaskDto.AssignedToId.HasValue &&
                !await _userRepository.ExistsAsync(createTaskDto.AssignedToId.Value))
            {
                throw new NotFoundException(nameof(User), createTaskDto.AssignedToId.Value);
            }

            var task = _mapper.Map<TaskEntity>(createTaskDto);
            task.Id = Guid.NewGuid();
            task.CreatedById = createdById;

            var createdTask = await _taskRepository.CreateAsync(task);
            return _mapper.Map<TaskDto>(createdTask);
        }

        public async Task<TaskDto> UpdateTaskAsync(Guid id, UpdateTaskDto updateTaskDto)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
            {
                throw new NotFoundException(nameof(TaskEntity), id);
            }

            // Verify assigned user exists (if provided)
            if (updateTaskDto.AssignedToId.HasValue &&
                !await _userRepository.ExistsAsync(updateTaskDto.AssignedToId.Value))
            {
                throw new NotFoundException(nameof(User), updateTaskDto.AssignedToId.Value);
            }

            _mapper.Map(updateTaskDto, task);
            await _taskRepository.UpdateAsync(task);

            var updatedTask = await _taskRepository.GetByIdAsync(id);
            return _mapper.Map<TaskDto>(updatedTask);
        }

        public async Task<TaskDto> UpdateTaskStatusAsync(Guid id, TaskEntityStatus status)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
            {
                throw new NotFoundException(nameof(TaskEntity), id);
            }

            task.Status = status;
            task.UpdatedAt = DateTime.UtcNow;
            await _taskRepository.UpdateAsync(task);

            var updatedTask = await _taskRepository.GetByIdAsync(id);
            return _mapper.Map<TaskDto>(updatedTask);
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            if (!await _taskRepository.ExistsAsync(id))
            {
                throw new NotFoundException(nameof(TaskEntity), id);
            }

            await _taskRepository.DeleteAsync(id);
        }
    }
}
