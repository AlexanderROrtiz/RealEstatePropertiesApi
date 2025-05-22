using AutoMapper;
using MediatR;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Interfaces;

namespace RealEstate.Properties.Application.Commands.OwnersCommand
{
    public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, OwnerDto>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public CreateOwnerCommandHandler(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task<OwnerDto> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            var owner = _mapper.Map<Domain.Entities.Owner>(request.Owner);
            await _ownerRepository.CreateAsync(owner);
            return _mapper.Map<OwnerDto>(owner);
        }
    }
}
