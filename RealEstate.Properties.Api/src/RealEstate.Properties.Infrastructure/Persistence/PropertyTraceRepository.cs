
using MongoDB.Driver;
using RealEstate.Properties.Application.Interfaces;
using RealEstate.Properties.Domain.Entities;

namespace RealEstate.Properties.Infrastructure.Persistence
{
    public class PropertyTraceRepository : IPropertyTraceRepository
    {
        private readonly IMongoCollection<PropertyTrace> _collection;

        public PropertyTraceRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<PropertyTrace>("PropertyTraces");
        }

        public async Task<List<PropertyTrace>> GetAllByPropertyIdAsync(string propertyId)
        {
            return await _collection.Find(x => x.IdProperty == propertyId).ToListAsync();
        }

        public async Task<PropertyTrace> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.IdPropertyTrace == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(PropertyTrace trace)
        {
            await _collection.InsertOneAsync(trace);
        }

        public async Task UpdateAsync(PropertyTrace trace)
        {
            await _collection.ReplaceOneAsync(x => x.IdPropertyTrace == trace.IdPropertyTrace, trace);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.IdPropertyTrace == id);
        }
    }
}