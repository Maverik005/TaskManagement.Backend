using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Entities
{
    public class Attachment
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public Guid UploadedById { get; set; }
        public DateTime UploadedAt { get; set; }

        // Navigation properties
        public TaskEntity Task { get; set; } = null!;
        public User UploadedBy { get; set; } = null!;
    }
}
