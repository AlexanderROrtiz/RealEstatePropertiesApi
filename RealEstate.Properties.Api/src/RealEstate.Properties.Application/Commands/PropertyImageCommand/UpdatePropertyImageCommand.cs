using MediatR;
using RealEstate.Properties.Application.DTOs;

namespace RealEstate.Properties.Application.Commands.PropertyImageCommand
{
    public class UpdatePropertyImageCommand : IRequest<PropertyImageDto>
    {
        public PropertyImageDto Image { get; }
        public UpdatePropertyImageCommand(PropertyImageDto image) => Image = image;
    }
}
