using MongoDB.Driver;
using RealEstate.Properties.Application.Interfaces;
using RealEstate.Properties.Domain.Entities;

namespace RealEstate.Properties.Infrastructure.Persistence
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly IMongoCollection<PropertyImage> _collection;

        public PropertyImageRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<PropertyImage>("PropertyImages");
        }

        public async Task<List<PropertyImage>> GetAllByPropertyIdAsync(string propertyId)
        {
            return await _collection.Find(x => x.IdProperty == propertyId).ToListAsync();
        }

        public async Task<PropertyImage> GetEnabledByPropertyIdAsync(string propertyId)
        {
            return await _collection.Find(x => x.IdProperty == propertyId && x.Enabled).FirstOrDefaultAsync();
        }

        public async Task<PropertyImage> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.IdPropertyImage == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(PropertyImage image)
        {
            await _collection.InsertOneAsync(image);
        }

        public async Task UpdateAsync(PropertyImage image)
        {
            await _collection.ReplaceOneAsync(x => x.IdPropertyImage == image.IdPropertyImage, image);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.IdPropertyImage == id);
        }
    }
}