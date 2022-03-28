using Autofac;
using Core.API.Authentication;
using Core.API.Mapping;

namespace Core.API.Ioc
{
    internal class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ViewMapper>().As<IViewMapper>()
                .SingleInstance();

            builder.RegisterType<AuthenticationService>().As<IAuthentication>().SingleInstance();
        }
    }
}
