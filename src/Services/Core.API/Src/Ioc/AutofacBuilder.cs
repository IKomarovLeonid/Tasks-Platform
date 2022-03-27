using Autofac;
using Core.API.Quartz;
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
            
            return builder;
        }
    }
}
