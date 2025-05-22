using RealEstate.Properties.Domain.Entities;

namespace RealEstate.Properties.Application.Interfaces
{
    public interface IPropertyRepository
    {
        Task<List<Property>> GetAllAsync();
        Task<Property> GetByIdAsync(string id);
        Task CreateAsync(Property property);
        Task UpdateAsync(Property property);
        Task DeleteAsync(string id);
        Task<List<Property>> FilterAsync(string name, string address, decimal? minPrice, decimal? maxPrice);
    }
}