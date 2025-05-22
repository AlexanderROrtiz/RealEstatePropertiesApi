using AutoMapper;
using MediatR;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Interfaces;
using RealEstate.Properties.Domain.Entities;

namespace RealEstate.Properties.Application.Commands.PropertyImageCommand
{
    public class UpdatePropertyImageCommandHandler : IRequestHandler<UpdatePropertyImageCommand, PropertyImageDto>
    {
        private readonly IPropertyImageRepository _repository;
        private readonly IMapper _mapper;

        public UpdatePropertyImageCommandHandler(IPropertyImageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PropertyImageDto> Handle(UpdatePropertyImageCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<PropertyImage>(request.Image);
            await _repository.UpdateAsync(entity);
            return _mapper.Map<PropertyImageDto>(entity);
        }
    }
}
