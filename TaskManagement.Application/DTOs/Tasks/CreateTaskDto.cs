
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs.Tasks
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public Guid? AssignedToId { get; set; }
        public Priority Priority { get; set; } = Priority.Medium;
        public DateTime? DueDate { get; set; }
    }
}
