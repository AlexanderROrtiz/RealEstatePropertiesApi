using AutoMapper;
using MediatR;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Interfaces;
using RealEstate.Properties.Application.Interfaces.Kafka;
using RealEstate.Properties.Application.Kafka.Interfaces;
using RealEstate.Properties.Domain.Entities;
using RealEstate.Properties.Domain.Events;

namespace RealEstate.Properties.Application.Commands.PropertiesCommand
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, PropertyDto>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyEventProducer _eventProducer;
        private readonly IEventSelector _eventSelector;
        private readonly IOwnerRepository _ownerRepository;

        public CreatePropertyCommandHandler(
            IPropertyRepository propertyRepository,
            IMapper mapper,
            IPropertyEventProducer eventProducer,
            IEventSelector eventSelector,
            IOwnerRepository ownerRepository)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
            _eventProducer = eventProducer;
            _eventSelector = eventSelector;
            _ownerRepository = ownerRepository;
        }

        public async Task<PropertyDto> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = _mapper.Map<Property>(request.PropertyDto);
            await _propertyRepository.CreateAsync(property);

            var owner = await _ownerRepository.GetByIdAsync(property.IdOwner);

            property.Owner = owner;
            var propertyDto = _mapper.Map<PropertyDto>(property);

            var evt = new PropertyCreatedEvent(property);
            await _eventProducer.PublishAsync(evt);

            Console.WriteLine($"Propiedad creada del (evento: {evt})");

            return _mapper.Map<PropertyDto>(property);
        }
    }
}
