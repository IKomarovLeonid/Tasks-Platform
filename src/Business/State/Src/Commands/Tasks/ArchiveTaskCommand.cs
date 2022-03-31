using Environment.Src;
using MediatR;

namespace State.Commands.Tasks
{
    public class ArchiveTaskCommand : IStateCommand
    {
        public ArchiveTaskCommand(ulong id)
        {
            Id = id;
        }

        public ulong Id { get; }
    }
}
