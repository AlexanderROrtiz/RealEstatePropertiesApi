using AutoMapper;
using MediatR;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Interfaces;
using RealEstate.Properties.Domain.Entities;

namespace RealEstate.Properties.Application.Commands.PropertyTraceCommand
{
    public class CreatePropertyTraceCommandHandler : IRequestHandler<CreatePropertyTraceCommand, PropertyTraceDto>
    {
        private readonly IPropertyTraceRepository _repository;
        private readonly IMapper _mapper;

        public CreatePropertyTraceCommandHandler(IPropertyTraceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PropertyTraceDto> Handle(CreatePropertyTraceCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<PropertyTrace>(request.Trace);
            await _repository.AddAsync(entity);
            return _mapper.Map<PropertyTraceDto>(entity);
        }
    }
}
