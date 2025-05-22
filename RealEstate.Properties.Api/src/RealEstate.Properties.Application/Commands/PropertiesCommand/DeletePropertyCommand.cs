
using MediatR;

namespace RealEstate.Properties.Application.Commands.PropertiesCommand
{
    public class DeletePropertyCommand : IRequest<bool>
    {
        public string PropertyId { get; set; }
        public DeletePropertyCommand(string propertyId)
        {
            PropertyId = propertyId;
        }
    }
}
