using AutoMapper;
using MediatR;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Interfaces;
using RealEstate.Properties.Domain.Entities;

namespace RealEstate.Properties.Application.Commands.PropertyImageCommand
{
    public class CreatePropertyImageCommandHandler : IRequestHandler<CreatePropertyImageCommand, PropertyImageDto>
    {
        private readonly IPropertyImageRepository _repository;
        private readonly IMapper _mapper;

        public CreatePropertyImageCommandHandler(IPropertyImageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PropertyImageDto> Handle(CreatePropertyImageCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<PropertyImage>(request.Image);
            await _repository.AddAsync(entity);
            return _mapper.Map<PropertyImageDto>(entity);
        }
    }
}