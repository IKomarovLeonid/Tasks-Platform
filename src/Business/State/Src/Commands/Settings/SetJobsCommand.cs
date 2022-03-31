using Environment.Src.State;

namespace State.Commands.Settings
{
    public class SetJobsCommand : BaseCommand
    {
        public double CheckTaskExpirationJobSec { get; set; }

        public SetJobsCommand() : base(nameof(SetJobsCommand)) { }
    }
}
