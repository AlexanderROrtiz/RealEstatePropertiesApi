
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RealEstate.Properties.Domain.Entities
{
    public class PropertyImage
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string IdPropertyImage { get; set; }
        public string IdProperty { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
    }
}
