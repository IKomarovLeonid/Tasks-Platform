﻿using Autofac;

namespace Core.API.Src.Ioc
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
