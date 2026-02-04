using System.Runtime.CompilerServices;
using MassTransit;
using Orders.Service.Application.Common.Messaging;
using Orders.Service.Infrastructure.Messaging;
using Orders.Service.Infrastructure.Messaging.Consumers;

namespace Orders.Service.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ConfirmOrderConsumer>();

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("orders-confirm", e =>
                    {
                        e.ConfigureConsumer<ConfirmOrderConsumer>(ctx);
                    });
                });
            });

            services.AddScoped<IEventBus, EventBus>();

            return services;

        }
    }
}
