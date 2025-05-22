
using AutoMapper;
using MediatR;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Interfaces;

namespace RealEstate.Properties.Application.Queries.PropertyTrace
{
    public class GetPropertyTracesByPropertyIdQueryHandler : IRequestHandler<GetPropertyTracesByPropertyIdQuery, IEnumerable<PropertyTraceDto>>
    {
        private readonly IPropertyTraceRepository _repository;
        private readonly IMapper _mapper;

        public GetPropertyTracesByPropertyIdQueryHandler(IPropertyTraceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PropertyTraceDto>> Handle(GetPropertyTracesByPropertyIdQuery request, CancellationToken cancellationToken)
        {
            var traces = await _repository.GetAllByPropertyIdAsync(request.PropertyId);
            return _mapper.Map<IEnumerable<PropertyTraceDto>>(traces);
        }
    }
}
