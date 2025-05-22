using RealEstate.Properties.Domain.Entities;

namespace RealEstate.Properties.Application.Interfaces
{
    public interface IOwnerRepository
    {
        Task<List<Owner>> GetAllAsync();
        Task<Owner> GetByIdAsync(string id);
        Task CreateAsync(Owner owner);
        Task UpdateAsync(Owner owner);
        Task DeleteAsync(string id);
    }
}
