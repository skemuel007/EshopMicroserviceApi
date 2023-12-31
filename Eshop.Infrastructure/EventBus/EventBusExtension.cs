using Eshop.Infrastructure.Query.Product;
using MassTransit;
using MassTransit.MultiBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Infrastructure.EventBus;

public static class EventBusExtension
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMq = new RabbitMqOption();
        configuration.GetSection("RabbitMq").Bind(rabbitMq);

        services.AddMassTransit(x =>
        {
            // https://medium.com/@bantyder/how-to-use-masstransit-8-0-13-rabbitmq-with-aspnetcore-7e199998b92d
            x.UsingRabbitMq((ctx,cfg)=>
            { 
                cfg.Host(new Uri(rabbitMq.ConnectionString),"/" , hostCfg => 
                { 
                    hostCfg.Username(rabbitMq.Username);
                    hostCfg.Password(rabbitMq.Password); 
                }); 
                cfg.ConfigureEndpoints(ctx);
            });
            
            // this is for product request
            x.AddRequestClient<GetProductById>();
        });

        return services;
    }
    
}