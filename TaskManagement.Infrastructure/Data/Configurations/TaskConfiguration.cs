using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Data.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .HasMaxLength(1000);

            builder.Property(t => t.Status)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(t => t.Priority)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(t => t.CreatedAt)
                .IsRequired();

            builder.Property(t => t.UpdatedAt)
                .IsRequired();

            // Indexes
            builder.HasIndex(t => t.ProjectId);
            builder.HasIndex(t => t.AssignedToId);
            builder.HasIndex(t => t.Status);
            builder.HasIndex(t => t.Priority);
            builder.HasIndex(t => t.DueDate);

            // Relationships
            builder.HasMany(t => t.Comments)
                .WithOne(c => c.Task)
                .HasForeignKey(c => c.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Attachments)
                .WithOne(a => a.Task)
                .HasForeignKey(a => a.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
