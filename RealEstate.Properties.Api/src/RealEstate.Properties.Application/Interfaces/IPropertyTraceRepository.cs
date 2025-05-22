
using RealEstate.Properties.Domain.Entities;

namespace RealEstate.Properties.Application.Interfaces
{
    public interface IPropertyTraceRepository
    {
        Task<List<PropertyTrace>> GetAllByPropertyIdAsync(string propertyId);
        Task<PropertyTrace> GetByIdAsync(string id);
        Task AddAsync(PropertyTrace trace);
        Task UpdateAsync(PropertyTrace trace);
        Task DeleteAsync(string id);
    }
}
