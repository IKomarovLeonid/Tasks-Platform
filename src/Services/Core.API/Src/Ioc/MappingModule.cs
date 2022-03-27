using Autofac;
using AutoMapper;
using Core.API.View.Tasks;
using Objects.Dto;

namespace Core.API.Ioc
{
    internal class MappingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TaskDto, TaskViewModel>();
            })).AsSelf().SingleInstance();

            builder.Register(c =>
                {
                    var context = c.Resolve<IComponentContext>();
                    var config = context.Resolve<MapperConfiguration>();
                    return config.CreateMapper(context.Resolve);
                })
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
