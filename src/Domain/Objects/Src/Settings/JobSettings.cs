using FluentValidation;
using FluentValidation.Results;

namespace Objects.Settings
{
    public class JobSettings
    {
        public int CheckTaskExpirationJobSec { get; set; }

        public int ReloadCachesJobSec { get; set; }

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

            RuleFor(t => t.ReloadCachesJobSec)
             .NotNull()
             .GreaterThan(0)
             .LessThanOrEqualTo(84000);
        }
    }


}
