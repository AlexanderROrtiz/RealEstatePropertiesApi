
namespace RealEstate.Properties.Application.Interfaces.Kafka
{
    public interface IEventSelector
    {
        string GetTopicForEvent(object domainEvent);
    }
}
