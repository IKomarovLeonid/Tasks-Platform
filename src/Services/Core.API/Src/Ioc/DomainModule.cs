using Autofac;
using Objects.Dto;
using Objects.Settings;
using Persistence.Src;
using Persistence.Src.Store;
using Persistence.Storage;

namespace Core.API.Ioc
{
    internal class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Store<TaskDto>>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<Storage<TaskDto>>()
                .AsImplementedInterfaces()
                .SingleInstance();


            builder.RegisterType<DomainManager<TaskDto>>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<SettingsStorage<BaseSettings>>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
