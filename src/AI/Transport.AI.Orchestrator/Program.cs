using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Transport.AI.Agents;
using Transport.AI.Orchestrator;
using Transport.AI.Orchestrator.Consumers;
using Transport.AI.Orchestrator.Infrastructure.OpenAIClient;
using Transport.AI.Orchestrator.Orchestration;
using Transport.AI.Orchestrator.Saga;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton(
    _ => new OpenAIClient(builder.Configuration["OpenAI:ApiKey"])
);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("ai-orchestrator-orders", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(ctx);
        });
    });
});

// Saga
builder.Services.AddSingleton<ISagaRepository, InMemorySagaRepository>();

// Coordinator
builder.Services.AddScoped<OrchestratorCoordinator>();

// Agents (fake for now)
builder.Services.AddSingleton<LogisticsAgent>();
builder.Services.AddSingleton<OperationsAgent>();
builder.Services.AddSingleton<FinanceAgent>();

await builder.Build().RunAsync();
