using RealEstate.Properties.Domain.Entities;

namespace RealEstate.Properties.Domain.Events
{
    public class PropertyUpdatedEvent
    {
        public Property Property { get; }
        public DateTime OccurredOn { get; }

        public PropertyUpdatedEvent(Property property)
        {
            Property = property;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
