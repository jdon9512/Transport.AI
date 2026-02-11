using MassTransit;

namespace Transport.AI.Infrastructure.Messaging;

public class EventBus : IEventBus
{
    private readonly IPublishEndpoint _publish;

    public EventBus(IPublishEndpoint publish)
    {
        _publish = publish;
    }

    public Task Publish<T>(T msg) where T : class =>
        _publish.Publish(msg);
}
