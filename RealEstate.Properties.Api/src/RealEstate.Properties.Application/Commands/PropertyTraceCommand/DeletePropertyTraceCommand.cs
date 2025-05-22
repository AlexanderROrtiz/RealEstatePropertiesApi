using MediatR;

namespace RealEstate.Properties.Application.Commands.PropertyTraceCommand
{
    public class DeletePropertyTraceCommand : IRequest<bool>
    {
        public string TraceId { get; }
        public DeletePropertyTraceCommand(string traceId) => TraceId = traceId;
    }
}
