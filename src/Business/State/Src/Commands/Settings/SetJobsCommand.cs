using Environment.Src;
using Environment.Src.State;
using MediatR;

namespace State.Commands.Settings
{
    public class SetJobsCommand : BaseCommand
    {
        public double CheckTaskExpirationJobSec { get; set; }

        public SetJobsCommand() : base(nameof(SetJobsCommand)) { }
    }
}
