using Autofac;
using Scheduler.Src;

namespace Core.API.Ioc
{
    internal class AutofacBuilder
    {
        public static ContainerBuilder Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<ServicesModule>();
            builder.RegisterModule<MappingModule>();
            builder.RegisterModule<ScheduleModule>();
            builder.RegisterModule<JobsModule>();
            
            return builder;
        }
    }
}
