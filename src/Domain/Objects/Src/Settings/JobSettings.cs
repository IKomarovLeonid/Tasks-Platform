using FluentValidation;
using FluentValidation.Results;

namespace Objects.Settings
{
    public class JobSettings
    {
        public double CheckTaskExpirationJobSec { get; set; }

        private static readonly IValidator<JobSettings> Validation = new JobsValidator();

        public ValidationResult Validate()
        {
            return Validation.Validate(this);
        }
    }

    class JobsValidator : AbstractValidator<JobSettings>
    {
        public JobsValidator()
        {
            RuleFor(t => t.CheckTaskExpirationJobSec)
                .NotNull()
                .GreaterThan(0)
                .LessThanOrEqualTo(84000);
        }
    }


}
