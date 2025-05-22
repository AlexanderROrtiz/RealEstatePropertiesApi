
using AutoMapper;
using MediatR;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Interfaces;

namespace RealEstate.Properties.Application.Queries.Owners
{
    public class GetOwnersQueryHandler : IRequestHandler<GetOwnersQuery, IEnumerable<OwnerDto>>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public GetOwnersQueryHandler(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OwnerDto>> Handle(GetOwnersQuery request, CancellationToken cancellationToken)
        {
            var owners = await _ownerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OwnerDto>>(owners);
        }
    }
}
