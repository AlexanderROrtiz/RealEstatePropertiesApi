using MediatR;
using RealEstate.Properties.Application.DTOs;

namespace RealEstate.Properties.Application.Commands.PropertyTraceCommand
{
    public class UpdatePropertyTraceCommand : IRequest<PropertyTraceDto>
    {
        public PropertyTraceDto Trace { get; }
        public UpdatePropertyTraceCommand(PropertyTraceDto trace) => Trace = trace;
    }
}
