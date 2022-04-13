using System;
using FluentValidation;
using FluentValidation.Results;

namespace Objects.Dto
{
    public class TaskDto : RootDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime? ExpirationUtc { get; set; }

        public string Category { get; set; }

        public Priority Priority { get; set; }

        private static readonly IValidator<TaskDto> Validation = new TaskValidator();

        public ValidationResult Validate()
        {
            return Validation.Validate(this);
        }
    }
    
    public enum TaskStatus
    {
        NotDefined,
        Pending,
        Processing,
        Expired,
        Processed
    }

    public enum Priority
    {
        NotDefined,
        Urgent,
        High,
        Medium,
        Low
    }

    class TaskValidator : AbstractValidator<TaskDto>
    {
        public TaskValidator()
        {
            RuleFor(t => t.Status).IsInEnum();

            RuleFor(t => t.Priority).IsInEnum();

            RuleFor(t => t.Title).NotNull()
                .MinimumLength(1)
                .MaximumLength(32);

            RuleFor(t => t.Description).NotNull()
                .MinimumLength(1)
                .MaximumLength(4096);

            RuleFor(t => t.Category)
                .MinimumLength(1)
                .MaximumLength(32)
                .When(t => t.Category != null);

            RuleFor(t => t.ExpirationUtc)
                .GreaterThanOrEqualTo(DateTime.UtcNow)
                .When(t => t.ExpirationUtc != null);
        }
    }
}
