using AutoMapper;
using MediatR;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Interfaces;

namespace RealEstate.Properties.Application.Queries.PropertyImage
{
    public class GetPropertyImagesByPropertyIdQueryHandler : IRequestHandler<GetPropertyImagesByPropertyIdQuery, IEnumerable<PropertyImageDto>>
    {
        private readonly IPropertyImageRepository _repository;
        private readonly IMapper _mapper;

        public GetPropertyImagesByPropertyIdQueryHandler(IPropertyImageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PropertyImageDto>> Handle(GetPropertyImagesByPropertyIdQuery request, CancellationToken cancellationToken)
        {
            var images = await _repository.GetAllByPropertyIdAsync(request.PropertyId);
            return _mapper.Map<IEnumerable<PropertyImageDto>>(images);
        }
    }
}
