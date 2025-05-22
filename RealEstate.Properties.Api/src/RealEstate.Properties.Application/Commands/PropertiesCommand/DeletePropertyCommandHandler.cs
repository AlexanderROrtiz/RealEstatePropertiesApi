using MediatR;
using RealEstate.Properties.Application.Interfaces;
using RealEstate.Properties.Application.Kafka.Interfaces;
using RealEstate.Properties.Domain.Events;

namespace RealEstate.Properties.Application.Commands.PropertiesCommand
{
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, bool>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyEventProducer _eventProducer;

        public DeletePropertyCommandHandler(
            IPropertyRepository propertyRepository,
            IPropertyEventProducer eventProducer)
        {
            _propertyRepository = propertyRepository;
            _eventProducer = eventProducer;
        }

        public async Task<bool> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            await _propertyRepository.DeleteAsync(request.PropertyId);

            var evt = new PropertyDeletedEvent(request.PropertyId);
            //var topic = EventSelector.GetTopicForEvent(evt); eventos por tipo validar
            await _eventProducer.PublishAsync(evt);

            return true;
        }
    }
}
