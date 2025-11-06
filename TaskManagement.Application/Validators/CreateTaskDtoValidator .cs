using FluentValidation;
using TaskManagement.Application.DTOs.Tasks;

namespace TaskManagement.Application.Validators
{
    public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Task title is required.")
                .MaximumLength(200).WithMessage("Task title must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("Project ID is required.");

            RuleFor(x => x.Priority)
                .IsInEnum().WithMessage("Invalid priority value.");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.")
                .When(x => x.DueDate.HasValue);
        }
    }
}
