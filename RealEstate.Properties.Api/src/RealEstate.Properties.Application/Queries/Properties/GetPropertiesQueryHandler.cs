using AutoMapper;
using MediatR;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Interfaces;

namespace RealEstate.Properties.Application.Queries.Properties
{
    public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, IEnumerable<PropertyDto>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetPropertiesQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PropertyDto>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = await _propertyRepository.FilterAsync(
                request.Name,
                request.Address,
                request.MinPrice,
                request.MaxPrice
            );
            return _mapper.Map<IEnumerable<PropertyDto>>(properties);
        }
    }
}
