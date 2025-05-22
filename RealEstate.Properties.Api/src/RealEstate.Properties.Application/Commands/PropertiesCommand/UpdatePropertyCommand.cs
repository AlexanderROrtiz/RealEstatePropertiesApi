using MediatR;
using RealEstate.Properties.Application.DTOs;

namespace RealEstate.Properties.Application.Commands.PropertiesCommand
{
    public class UpdatePropertyCommand : IRequest<PropertyDto>
    {
        public PropertyDto Property { get; set; }
        public UpdatePropertyCommand(PropertyDto property)
        {
            Property = property;
        }
    }
}
