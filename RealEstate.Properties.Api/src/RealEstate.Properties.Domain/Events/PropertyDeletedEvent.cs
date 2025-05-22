
namespace RealEstate.Properties.Domain.Events
{
    public class PropertyDeletedEvent
    {
        public string PropertyId { get; }
        public DateTime OccurredOn { get; }

        public PropertyDeletedEvent(string propertyId)
        {
            PropertyId = propertyId;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
