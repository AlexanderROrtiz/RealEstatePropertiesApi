
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RealEstate.Properties.Domain.Entities
{
    public class PropertyTrace
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
        public string IdProperty { get; set; }
    }
}
