using System;
using Environment.Src.State;

namespace State.Commands.Tasks
{
    public class CreateTaskCommand : BaseCommand
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ExpirationUtc { get; set; }

        public CreateTaskCommand(): base(nameof(CreateTaskCommand)) { }
    }
}
