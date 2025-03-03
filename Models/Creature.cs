using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CompulsoryREST.Models;
 
public class Creature {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]    
    public string? Name { get; set; }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Species { get; set; }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Location { get; set; }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int? Lifespan { get; set; } // in years

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Size { get; set; }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Status { get; set; }
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Description { get; set; }
}