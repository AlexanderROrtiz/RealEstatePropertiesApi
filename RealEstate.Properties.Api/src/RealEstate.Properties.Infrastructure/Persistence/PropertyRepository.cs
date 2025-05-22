
using MongoDB.Driver;
using RealEstate.Properties.Application.Interfaces;
using RealEstate.Properties.Domain.Entities;

namespace RealEstate.Properties.Infrastructure.Persistence
{
    public class PropertyRepository: IPropertyRepository
    {
        private readonly IMongoCollection<Property> _collection;

        public PropertyRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<Property>("Properties");
        }

        public async Task<List<Property>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Property> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.IdProperty == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Property property)
        {
            await _collection.InsertOneAsync(property);
        }

        public async Task UpdateAsync(Property property)
        {
            await _collection.ReplaceOneAsync(x => x.IdProperty == property.IdProperty, property);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.IdProperty == id);
        }

        // Métodos de filtro por nombre, dirección, precio
        public async Task<List<Property>> FilterAsync(string name, string address, decimal? minPrice, decimal? maxPrice)
        {
            var filter = Builders<Property>.Filter.Empty;

            if (!string.IsNullOrEmpty(name))
                filter &= Builders<Property>.Filter.Regex(nameof(Property.Name), new MongoDB.Bson.BsonRegularExpression(name, "i"));
            if (!string.IsNullOrEmpty(address))
                filter &= Builders<Property>.Filter.Regex(nameof(Property.Address), new MongoDB.Bson.BsonRegularExpression(address, "i"));
            if (minPrice.HasValue)
                filter &= Builders<Property>.Filter.Gte(nameof(Property.Price), minPrice.Value);
            if (maxPrice.HasValue)
                filter &= Builders<Property>.Filter.Lte(nameof(Property.Price), maxPrice.Value);

            return await _collection.Find(filter).ToListAsync();
        }
    }
}