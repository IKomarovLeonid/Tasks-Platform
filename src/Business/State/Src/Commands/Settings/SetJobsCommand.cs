using Environment.State;

namespace State.Commands.Settings
{
    public class SetJobsCommand : BaseCommand
    {
        public int CheckTaskExpirationJobSec { get; set; }

        public SetJobsCommand() : base(nameof(SetJobsCommand)) { }
    }
}
