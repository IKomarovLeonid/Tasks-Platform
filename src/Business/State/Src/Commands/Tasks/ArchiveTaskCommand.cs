using Environment.Src;
using Environment.Src.State;
using MediatR;

namespace State.Commands.Tasks
{
    public class ArchiveTaskCommand : BaseCommand
    {
        public ArchiveTaskCommand(ulong id) : base(nameof(ArchiveTaskCommand))
        {
            Id = id;
        }

        public ulong Id { get; }
    }
}
