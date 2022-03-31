using System;
using MediatR;
using State;

namespace Environment.Src
{
    public interface IStateCommand : IRequest<StateResult>
    {

    }
}
