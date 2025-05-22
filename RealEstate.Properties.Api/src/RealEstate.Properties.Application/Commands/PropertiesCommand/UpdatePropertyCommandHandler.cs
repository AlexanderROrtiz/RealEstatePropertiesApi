using AutoMapper;
using MediatR;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Interfaces;
using RealEstate.Properties.Application.Kafka.Interfaces;
using RealEstate.Properties.Domain.Entities;
using RealEstate.Properties.Domain.Events;

namespace RealEstate.Properties.Application.Commands.PropertiesCommand
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, PropertyDto>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyEventProducer _eventProducer;

        public UpdatePropertyCommandHandler(
            IPropertyRepository propertyRepository,
            IMapper mapper,
            IPropertyEventProducer eventProducer)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
            _eventProducer = eventProducer;
        }

        public async Task<PropertyDto> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = _mapper.Map<Property>(request.Property);
            await _propertyRepository.UpdateAsync(property);

            var evt = new PropertyUpdatedEvent(property);
            //var topic = EventSelector.GetTopicForEvent(evt); eventos por tipo validar
            await _eventProducer.PublishAsync(evt);

            return _mapper.Map<PropertyDto>(property);
        }
    }
}
