
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RealEstate.Properties.Domain.Entities
{
    public class Property
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public string IdOwner { get; set; }

        // Navegación (opcional, útil para agregados)
        public Owner Owner { get; set; }
        public List<PropertyImage> Images { get; set; } = new();
        public List<PropertyTrace> Traces { get; set; } = new();
    }
}
