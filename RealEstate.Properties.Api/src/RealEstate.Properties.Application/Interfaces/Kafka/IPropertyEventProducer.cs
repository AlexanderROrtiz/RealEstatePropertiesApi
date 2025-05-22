
namespace RealEstate.Properties.Application.Kafka.Interfaces
{
    public interface IPropertyEventProducer
    {
        Task PublishAsync<T>(T @event);
    }
}
