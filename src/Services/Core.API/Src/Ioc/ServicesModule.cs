using Autofac;
using Core.API.Mapping;
using Environment.Src.Implementation;

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
        }
    }
}
