using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CompulsoryREST.Models;
    public class User
    {
     [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; } = Guid.NewGuid().ToString();
        [BsonElement("Username")]
        public string?  Username { get; set; } = string.Empty;
         [BsonElement("Password")]
        public string? Password { get; set; } = string.Empty;
    }