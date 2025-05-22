using MediatR;
using RealEstate.Properties.Application.DTOs;

namespace RealEstate.Properties.Application.Queries.PropertyImage
{
    public class GetPropertyImagesByPropertyIdQuery : IRequest<IEnumerable<PropertyImageDto>>
    {
        public string PropertyId { get; }
        public GetPropertyImagesByPropertyIdQuery(string propertyId) => PropertyId = propertyId;
    }
}
