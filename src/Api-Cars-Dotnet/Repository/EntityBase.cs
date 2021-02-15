using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api_Cars_Dotnet
{
    public abstract class EntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
    }
}