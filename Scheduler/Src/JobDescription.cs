using Quartz;

namespace Scheduler.Src
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
