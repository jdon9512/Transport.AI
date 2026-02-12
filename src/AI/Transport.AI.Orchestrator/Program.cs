using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Transport.AI.Agents;
using Transport.AI.Agents.AIClient;
using Transport.AI.Agents.Saga;
using Transport.AI.Infrastructure.Messaging;
using Transport.AI.Infrastructure.OpenAIClient;
using Transport.AI.Infrastructure.Saga;
using Transport.AI.Orchestrator.Consumers;
using Transport.AI.Orchestrator.Orchestration;
using OpenAI.Chat;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
        .AddJsonFile($"appsettings.Development.json", optional: true)
    .AddEnvironmentVariables();



builder.Services.AddSingleton<IAIClient, OwnOpenAIClient>();

builder.Services.AddSingleton<ChatClient>(serviceProvider =>
{
    var apiKey = builder.Configuration["OpenAI:ApiKey"];
    var model = "gpt-4o-mini";

    return new ChatClient(model, apiKey);
});

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

// infrastructure 
builder.Services.AddSingleton<ISagaRepository, InMemorySagaRepository>();
builder.Services.AddSingleton<IEventBus, EventBus>();

// Coordinator
builder.Services.AddScoped<OrchestratorCoordinator>();

// Agents (fake for now)
builder.Services.AddSingleton<LogisticsAgent>();
builder.Services.AddSingleton<OperationsAgent>();
builder.Services.AddSingleton<FinanceAgent>();

await builder.Build().RunAsync();
