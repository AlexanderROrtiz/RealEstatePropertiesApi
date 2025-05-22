using MediatR;
using RealEstate.Properties.Application.Interfaces;

namespace RealEstate.Properties.Application.Commands.OwnersCommand
{
    public class DeleteOwnerCommandHandler : IRequestHandler<DeleteOwnerCommand, bool>
    {
        private readonly IOwnerRepository _ownerRepository;

        public DeleteOwnerCommandHandler(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<bool> Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
        {
            await _ownerRepository.DeleteAsync(request.Id);
            return true;
        }
    }
}
