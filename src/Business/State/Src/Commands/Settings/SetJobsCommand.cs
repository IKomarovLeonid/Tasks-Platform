using MediatR;

namespace State.Commands.Settings
{
    public class SetJobsCommand : IRequest<StateResult>
    {
        public double CheckTaskExpirationJobSec { get; set; }

        public static SetJobsCommand DefaultCommand() => new()
        {
            CheckTaskExpirationJobSec = 60
        };
    }
}
