
using AutoMapper;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Domain.Entities;

namespace RealEstate.Properties.Infrastructure.Mappings
{
    public class PropertyMappings : Profile
    {
        public PropertyMappings()
        {
            CreateMap<Property, PropertyDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdProperty))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.IdOwner))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner != null ? src.Owner.Name : string.Empty))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src =>
                    src.Images != null && src.Images.Count > 0
                        ? src.Images[0].File
                        : string.Empty
                ));

            CreateMap<PropertyDto, Property>()
                .ForMember(dest => dest.IdProperty, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IdOwner, opt => opt.MapFrom(src => src.OwnerId))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.ImageUrl)
                        ? new List<PropertyImage> { new PropertyImage { File = src.ImageUrl, Enabled = true } }
                        : new List<PropertyImage>()
                ))
                .ForMember(dest => dest.Owner, opt => opt.Ignore());

            CreateMap<Owner, OwnerDto>()
                .ForMember(dest => dest.IdOwner, opt => opt.MapFrom(src => src.IdOwner));

            CreateMap<OwnerDto, Owner>()
                .ForMember(dest => dest.IdOwner, opt => opt.MapFrom(src => src.IdOwner));

            CreateMap<PropertyImage, PropertyImageDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdPropertyImage))
                .ForMember(dest => dest.PropertyId, opt => opt.MapFrom(src => src.IdProperty));

            CreateMap<PropertyTrace, PropertyTraceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdPropertyTrace))
                .ForMember(dest => dest.PropertyId, opt => opt.MapFrom(src => src.IdProperty));
        }
    }
}