
using RealEstate.Properties.Application.DTOs;

namespace RealEstate.Properties.Application.Interfaces
{
    public interface IOwnerService
    {
        Task<IEnumerable<OwnerDto>> GetOwnersAsync();
        Task<OwnerDto> GetOwnerByIdAsync(string id);
        Task<OwnerDto> CreateOwnerAsync(OwnerDto owner);
        Task<OwnerDto> UpdateOwnerAsync(OwnerDto owner);
        Task<bool> DeleteOwnerAsync(string id);
    }
}
