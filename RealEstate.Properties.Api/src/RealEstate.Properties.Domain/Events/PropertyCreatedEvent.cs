
using RealEstate.Properties.Domain.Entities;

namespace RealEstate.Properties.Domain.Events
{
    public class PropertyCreatedEvent
    {
        public Property Property { get; }
        public DateTime OccurredOn { get; }

        public PropertyCreatedEvent(Property property)
        {
            Property = property;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
