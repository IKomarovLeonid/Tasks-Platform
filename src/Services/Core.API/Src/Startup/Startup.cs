﻿using System;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Core.API.Configuration;
using Core.API.Ioc;
using Core.API.View.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Persistence;
using Queries.Src;
using State.Src;

namespace Core.API.Startup
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(t => ConfigurationReader.ReadConfig<ApplicationConfiguration>());

            services.AddDbContext<ApplicationContext>();

            services.AddCors();

            services.AddMvcCore(options => options.EnableEndpointRouting = false)
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                .AddApiExplorer();

            services.AddSwaggerDocument(settings =>
            {
                settings.SchemaType = NJsonSchema.SchemaType.OpenApi3;
                settings.AllowReferencesWithProperties = true;
                settings.Title = "Tasks-Platform";
            });

            var builder = AutofacBuilder.Build();

            // mediator 
            var stateAssembly = AppDomain.CurrentDomain.Load(StateAssembly.Value);
            var queriesAssembly = AppDomain.CurrentDomain.Load(QueriesAssembly.Value);
            services.AddMediatR(stateAssembly, queriesAssembly);
            services.AddHostedService<HostedService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

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
