using Environment.State;
using MediatR;

namespace Environment
{
    public interface IStateCommand : IRequest<StateResult>
    {
        string Name { get; }
    }
}
