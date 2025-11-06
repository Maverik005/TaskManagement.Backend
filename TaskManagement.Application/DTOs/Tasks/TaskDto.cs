
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs.Tasks
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public Guid? AssignedToId { get; set; }
        public string? AssignedToName { get; set; }
        public Guid CreatedById { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        public TaskEntityStatus Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CommentCount { get; set; }
        public int AttachmentCount { get; set; }
    }
}
