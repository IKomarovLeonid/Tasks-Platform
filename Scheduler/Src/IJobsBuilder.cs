using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduler.Src
{
    public interface IJobsBuilder
    {
        public Task<ICollection<JobDescription>> BuildJobs();
    }
}
