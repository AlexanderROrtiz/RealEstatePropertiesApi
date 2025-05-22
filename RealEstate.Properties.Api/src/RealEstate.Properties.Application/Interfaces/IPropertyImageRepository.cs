using RealEstate.Properties.Domain.Entities;

namespace RealEstate.Properties.Application.Interfaces
{
    public interface IPropertyImageRepository
    {
        Task<List<PropertyImage>> GetAllByPropertyIdAsync(string propertyId);
        Task<PropertyImage> GetEnabledByPropertyIdAsync(string propertyId);
        Task<PropertyImage> GetByIdAsync(string id);
        Task AddAsync(PropertyImage image);
        Task UpdateAsync(PropertyImage image);
        Task DeleteAsync(string id);
    }
}
