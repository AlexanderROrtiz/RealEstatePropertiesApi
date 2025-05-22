
using MediatR;
using RealEstate.Properties.Application.Interfaces;

namespace RealEstate.Properties.Application.Commands.PropertyTraceCommand
{
    public class DeletePropertyTraceCommandHandler : IRequestHandler<DeletePropertyTraceCommand, bool>
    {
        private readonly IPropertyTraceRepository _repository;

        public DeletePropertyTraceCommandHandler(IPropertyTraceRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeletePropertyTraceCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.TraceId);
            return true;
        }
    }
}
