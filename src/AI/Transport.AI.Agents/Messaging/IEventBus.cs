namespace Transport.AI.Infrastructure.Messaging;

public interface IEventBus
{
    Task Publish<T>(T message) where T : class;
}
