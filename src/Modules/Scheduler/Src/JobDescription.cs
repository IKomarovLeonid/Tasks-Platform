using Quartz;

namespace Scheduler
{
    public class JobDescription
    {
        public readonly ITrigger Trigger;

        public readonly IJobDetail JobDetail;

        public JobDescription(ITrigger trigger, IJobDetail jobDetail)
        {
            Trigger = trigger;
            JobDetail = jobDetail;
        }


    }
}
