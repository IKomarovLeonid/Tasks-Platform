using Environment.Src;
using MediatR;

namespace State.Commands.Settings
{
    public class SetJobsCommand : IStateCommand
    {
        public double CheckTaskExpirationJobSec { get; set; }
    }
}
