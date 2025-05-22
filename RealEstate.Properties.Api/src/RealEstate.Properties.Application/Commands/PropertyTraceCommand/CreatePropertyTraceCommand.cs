using MediatR;
using RealEstate.Properties.Application.DTOs;

namespace RealEstate.Properties.Application.Commands.PropertyTraceCommand
{
    public class CreatePropertyTraceCommand : IRequest<PropertyTraceDto>
    {
        public PropertyTraceDto Trace { get; }
        public CreatePropertyTraceCommand(PropertyTraceDto trace) => Trace = trace;
    }
}
