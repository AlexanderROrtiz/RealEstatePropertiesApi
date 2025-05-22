using MediatR;
using RealEstate.Properties.Application.DTOs;

namespace RealEstate.Properties.Application.Queries.Owners
{
    public class GetOwnerByIdQuery : IRequest<OwnerDto>
    {
        public string Id { get; }
        public GetOwnerByIdQuery(string id) => Id = id;
    }
}
