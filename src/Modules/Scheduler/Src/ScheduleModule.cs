using Autofac;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Scheduler
{
    public class ScheduleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QuartzService>().AsSelf().SingleInstance();
            builder.RegisterType<JobFactory>().As<IJobFactory>().SingleInstance();
            builder.RegisterType<StdSchedulerFactory>().As<ISchedulerFactory>().SingleInstance();
            builder.Register(ctx =>
            {
                var schedulerFactory = ctx.Resolve<ISchedulerFactory>();
                var jobFactory = ctx.Resolve<IJobFactory>();

                var scheduler = schedulerFactory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();
                scheduler.JobFactory = jobFactory;
                return scheduler;
            });
        }
    }
}