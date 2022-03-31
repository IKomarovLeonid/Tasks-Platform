using MediatR;

namespace State.Commands.Tasks
{
    public class ArchiveTaskCommand : IRequest<StateResult>
    {
        public ArchiveTaskCommand(ulong id)
        {
            Id = id;
        }

        public ulong Id { get; }
    }
}
