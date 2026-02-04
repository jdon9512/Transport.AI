namespace Billing.Service.Application.Common.Messaging;

public interface IEventBus
{
    Task Publish<T>(T message) where T : class;
}
