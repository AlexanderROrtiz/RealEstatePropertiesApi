using MediatR;
using RealEstate.Properties.Application.Interfaces;

namespace RealEstate.Properties.Application.Commands.PropertyImageCommand
{
    public class DeletePropertyImageCommandHandler : IRequestHandler<DeletePropertyImageCommand, bool>
    {
        private readonly IPropertyImageRepository _repository;

        public DeletePropertyImageCommandHandler(IPropertyImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeletePropertyImageCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.ImageId);
            return true;
        }
    }
}
