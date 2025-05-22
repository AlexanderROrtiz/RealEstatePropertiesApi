
using MongoDB.Driver;
using RealEstate.Properties.Application.Interfaces;
using RealEstate.Properties.Domain.Entities;

namespace RealEstate.Properties.Infrastructure.Persistence
{
    public class OwnerRepository: IOwnerRepository
    {
        private readonly IMongoCollection<Owner> _collection;

        public OwnerRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<Owner>("Owners");
        }

        public async Task<List<Owner>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Owner> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.IdOwner == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Owner owner)
        {
            await _collection.InsertOneAsync(owner);
        }

        public async Task UpdateAsync(Owner owner)
        {
            await _collection.ReplaceOneAsync(x => x.IdOwner == owner.IdOwner, owner);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.IdOwner == id);
        }
    }
}