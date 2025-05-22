using MediatR;
using RealEstate.Properties.Application.DTOs;

namespace RealEstate.Properties.Application.Queries.Properties
{
    public class GetPropertyByIdQuery : IRequest<PropertyDto>
    {
        public string PropertyId { get; set; }

        public GetPropertyByIdQuery(string propertyId)
        {
            PropertyId = propertyId;
        }
    }
}
