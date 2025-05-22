using AutoMapper;
using MediatR;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Interfaces;

namespace RealEstate.Properties.Application.Queries.Owners
{
    public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, OwnerDto>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public GetOwnerByIdQueryHandler(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task<OwnerDto> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.GetByIdAsync(request.Id);
            return _mapper.Map<OwnerDto>(owner);
        }
    }
}
