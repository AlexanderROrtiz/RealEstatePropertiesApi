using RealEstate.Properties.Application.DTOs;

namespace RealEstate.Properties.Application.Interfaces
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyDto>> GetPropertiesAsync(string name = null, string address = null, decimal? minPrice = null, decimal? maxPrice = null);
        Task<PropertyDto> GetPropertyByIdAsync(string id);
        Task<PropertyDto> CreatePropertyAsync(PropertyDto property);
        Task<PropertyDto> UpdatePropertyAsync(PropertyDto property);
        Task<bool> DeletePropertyAsync(string id);
    }
}
