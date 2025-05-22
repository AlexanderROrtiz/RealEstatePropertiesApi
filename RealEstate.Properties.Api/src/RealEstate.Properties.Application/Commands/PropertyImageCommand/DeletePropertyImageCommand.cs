using MediatR;

namespace RealEstate.Properties.Application.Commands.PropertyImageCommand
{
    public class DeletePropertyImageCommand : IRequest<bool>
    {
        public string ImageId { get; }
        public DeletePropertyImageCommand(string imageId) => ImageId = imageId;
    }
}

