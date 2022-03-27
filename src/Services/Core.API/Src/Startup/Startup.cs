using System;
using Autofac.Extensions.DependencyInjection;
using Core.API.Src.Ioc;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.API.Src.Startup
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddMvcCore(options => options.EnableEndpointRouting = false)
                .AddJsonOptions(
                    options => options.JsonSerializerOptions.PropertyNamingPolicy = null
                    )
                .AddApiExplorer();

            services.AddSwaggerDocument(settings =>
            {
                settings.SchemaType = NJsonSchema.SchemaType.OpenApi3;
                settings.AllowReferencesWithProperties = true;
                settings.Title = "Tasks-Platform";
            });

            var builder = AutofacBuilder.Build();

            // mediator 
            var stateAssembly = AppDomain.CurrentDomain.Load("State");
            var queriesAssembly = AppDomain.CurrentDomain.Load("Queries");
            services.AddMediatR(stateAssembly, queriesAssembly);

            builder.Populate(services);
            return new AutofacServiceProvider(builder.Build());
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(_ => _.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            app.UseMvcWithDefaultRoute();

            app.UseOpenApi().UseSwaggerUi3();
        }
    }
}
