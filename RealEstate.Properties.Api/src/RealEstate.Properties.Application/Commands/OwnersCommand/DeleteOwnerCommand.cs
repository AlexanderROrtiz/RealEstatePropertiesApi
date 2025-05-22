using MediatR;

namespace RealEstate.Properties.Application.Commands.OwnersCommand
{
    public class DeleteOwnerCommand : IRequest<bool>
    {
        public string Id { get; }
        public DeleteOwnerCommand(string id) => Id = id;
    }
}
