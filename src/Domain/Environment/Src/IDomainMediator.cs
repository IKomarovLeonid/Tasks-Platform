using System.Threading.Tasks;
using Environment.State;

namespace Environment
{
    public interface IDomainMediator
    {
        Task<StateResult> SendAsync(IStateCommand command);
    }
}
