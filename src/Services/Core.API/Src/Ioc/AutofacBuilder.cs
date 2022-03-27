using Autofac;

namespace Core.API.Ioc
{
    internal class AutofacBuilder
    {
        public static ContainerBuilder Build()
        {
            var builder = new ContainerBuilder();
            
            return builder;
        }
    }
}
