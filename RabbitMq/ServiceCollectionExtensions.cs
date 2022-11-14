using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitMq;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureRabbit(
        this IServiceCollection services, Assembly? ass = null)
    {
        services.AddMassTransit(x =>
        {
            if (ass is not null)
            {
                x.AddConsumers(ass);
            }

            x.UsingRabbitMq((context, cfg) =>
            {
                //rabbitmq-broker
                //localhost
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}