﻿using System;
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
        Processed
    }

    class TaskValidator : AbstractValidator<TaskDto>
    {
        public TaskValidator()
        {
            RuleFor(t => t.Status).IsInEnum();

            RuleFor(t => t.Title).NotNull()
                .MinimumLength(1)
                .MaximumLength(32);

            RuleFor(t => t.Description).NotNull()
                .MinimumLength(1)
                .MaximumLength(4096);

            RuleFor(t => t.ExpirationUtc)
                .GreaterThanOrEqualTo(DateTime.UtcNow)
                .When(t => t.ExpirationUtc != null);
        }
    }
}
