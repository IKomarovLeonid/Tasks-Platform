using System;
using System.Threading.Tasks;
using State;

namespace Environment.Src
{
    public interface IDomainMediator
    {
        Task<StateResult> SendAsync(IStateCommand command);
    }
}
