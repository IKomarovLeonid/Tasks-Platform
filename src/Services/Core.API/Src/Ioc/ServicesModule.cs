using Autofac;
using Core.API.Mapping;
using Environment.Implementation;
using Processing.Listeners;

namespace Core.API.Ioc
{
    internal class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ViewMapper>().As<IViewMapper>()
                .SingleInstance();

            builder.RegisterType<DomainMediator>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<QueryMediator>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<JobsListener>().AsImplementedInterfaces().SingleInstance();
        }
    }
}
