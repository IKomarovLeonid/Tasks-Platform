using Autofac;
using Core.API.Jobs;
using Processing.Jobs;

namespace Core.API.Ioc
{
    internal class JobsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JobsBuilder>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<CheckTaskExpirationJob>().AsSelf().SingleInstance();
        }
    }
}
