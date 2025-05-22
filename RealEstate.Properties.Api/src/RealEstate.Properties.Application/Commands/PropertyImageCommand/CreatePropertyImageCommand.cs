using MediatR;
using RealEstate.Properties.Application.DTOs;

namespace RealEstate.Properties.Application.Commands.PropertyImageCommand
{
    public class CreatePropertyImageCommand : IRequest<PropertyImageDto>
    {
        public PropertyImageDto Image { get; }
        public CreatePropertyImageCommand(PropertyImageDto image) => Image = image;
    }
}
