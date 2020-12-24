using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api_Cars_Dotnet.Models
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int Age { get; set; }
    }
}