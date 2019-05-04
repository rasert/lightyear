using System;
using Lightyear.Catalog.Application.Abstractions;
using Lightyear.Catalog.Application.Services;
using Lightyear.Catalog.Infra.Data;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Lightyear.Catalog.IoC
{
    public static class IOCContainerConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICatalogUow, CatalogUow>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<CatalogContext>();
            services.AddScoped<IProductService, ProductService>();

            // Register MassTransit
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri("localhost"), hostConfigurator =>
                    {
                        hostConfigurator.Username("guest");
                        hostConfigurator.Password("guest");
                    });
                }));
            });
        }
    }
}
