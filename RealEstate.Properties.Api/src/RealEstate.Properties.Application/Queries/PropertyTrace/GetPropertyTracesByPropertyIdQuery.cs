using MediatR;
using RealEstate.Properties.Application.DTOs;

namespace RealEstate.Properties.Application.Queries.PropertyTrace
{
    public class GetPropertyTracesByPropertyIdQuery : IRequest<IEnumerable<PropertyTraceDto>>
    {
        public string PropertyId { get; }
        public GetPropertyTracesByPropertyIdQuery(string propertyId) => PropertyId = propertyId;
    }
}

