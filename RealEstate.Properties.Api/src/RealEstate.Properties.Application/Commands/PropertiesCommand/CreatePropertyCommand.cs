using MediatR;
using RealEstate.Properties.Application.DTOs;

namespace RealEstate.Properties.Application.Commands.PropertiesCommand
{
    public class CreatePropertyCommand : IRequest<PropertyDto>
    {
        public PropertyDto PropertyDto { get; }
        public CreatePropertyCommand(PropertyDto propertyDto) => PropertyDto = propertyDto;
    }
}
