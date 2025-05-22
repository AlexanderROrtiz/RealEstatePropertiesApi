using RealEstate.Properties.Application.Interfaces.Kafka;
using RealEstate.Properties.Domain.Events;

namespace RealEstate.Properties.Infrastructure.SelectorTemplate
{
    public class EventSelector: IEventSelector
    {
        public string GetTopicForEvent(object domainEvent)
        {
            // se peude mejorar con un diccionario eso es solo para probar
            Console.WriteLine($"Inicio el proceso del selector");
            return domainEvent switch
            {
                PropertyCreatedEvent => "property-created",
                PropertyUpdatedEvent => "property-updated",
                PropertyDeletedEvent => "property-deleted",
                //OwnerCreatedEvent => "owner-created",
                //OwnerUpdatedEvent => "owner-updated",
                //OwnerDeletedEvent => "owner-deleted",
                _ => throw new ArgumentException("Evento desconocido")
            };
        }
    }
}
