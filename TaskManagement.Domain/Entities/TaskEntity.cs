using System;
using System.Collections.Generic;

using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public Guid? AssignedToId { get; set; }
        public Guid CreatedById { get; set; }
        public TaskManagement.Domain.Enums.TaskEntityStatus Status { get; set; } = TaskManagement.Domain.Enums.TaskEntityStatus.ToDo;
        public Priority Priority { get; set; } = Priority.Medium;
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Project Project { get; set; } = null!;
        public User? AssignedTo { get; set; }
        public User CreatedBy { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
