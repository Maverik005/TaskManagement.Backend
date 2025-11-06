using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Data.Configurations
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.FileUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(a => a.ContentType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.FileSize)
                .IsRequired();

            builder.Property(a => a.UploadedAt)
                .IsRequired();

            // Indexes
            builder.HasIndex(a => a.TaskId);
        }
    }
}
