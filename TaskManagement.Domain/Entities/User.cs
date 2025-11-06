using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Member;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<Project> OwnedProjects { get; set; } = new List<Project>();
        public ICollection<TaskEntity> CreatedTasks { get; set; } = new List<TaskEntity>();
        public ICollection<TaskEntity> AssignedTasks { get; set; } = new List<TaskEntity>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    } 
}
