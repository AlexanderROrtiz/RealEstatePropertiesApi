
using MediatR;
using RealEstate.Properties.Application.DTOs;

namespace RealEstate.Properties.Application.Queries.Owners
{
    public class GetOwnersQuery : IRequest<IEnumerable<OwnerDto>> { }
}
