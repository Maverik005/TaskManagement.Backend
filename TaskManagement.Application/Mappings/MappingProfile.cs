using AutoMapper;
using TaskManagement.Application.DTOs.Projects;
using TaskManagement.Application.DTOs.Tasks;
using TaskManagement.Application.DTOs.Users;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

            // Project mappings
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.OwnerName,
                    opt => opt.MapFrom(src => $"{src.Owner.FirstName} {src.Owner.LastName}"))
                .ForMember(dest => dest.TaskCount,
                    opt => opt.MapFrom(src => src.Tasks.Count));

            CreateMap<CreateProjectDto, Project>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<UpdateProjectDto, Project>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            // Task mappings
            CreateMap<TaskEntity, TaskDto>()
                .ForMember(dest => dest.ProjectName,
                    opt => opt.MapFrom(src => src.Project.Name))
                .ForMember(dest => dest.AssignedToName,
                    opt => opt.MapFrom(src => src.AssignedTo != null
                        ? $"{src.AssignedTo.FirstName} {src.AssignedTo.LastName}"
                        : null))
                .ForMember(dest => dest.CreatedByName,
                    opt => opt.MapFrom(src => $"{src.CreatedBy.FirstName} {src.CreatedBy.LastName}"))
                .ForMember(dest => dest.CommentCount,
                    opt => opt.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.AttachmentCount,
                    opt => opt.MapFrom(src => src.Attachments.Count));

            CreateMap<CreateTaskDto, TaskEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => Domain.Enums.TaskEntityStatus.ToDo))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<UpdateTaskDto, TaskEntity>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
