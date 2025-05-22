using AutoMapper;
using MediatR;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Interfaces;

namespace RealEstate.Properties.Application.Commands.OwnersCommand
{
    public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand, OwnerDto>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public UpdateOwnerCommandHandler(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task<OwnerDto> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {
            var owner = _mapper.Map<Domain.Entities.Owner>(request.Owner);
            await _ownerRepository.UpdateAsync(owner);
            return _mapper.Map<OwnerDto>(owner);
        }
    }
}
