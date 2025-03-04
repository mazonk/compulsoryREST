using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CompulsoryREST.Models;
 
public class Creature {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("name")]    
    public string? Name { get; set; }

    [BsonElement("location")]
    public string? Location { get; set; }

        [BsonElement("description")]
    public string? Description { get; set; }

        [BsonElement("lifespan")]
    public int Lifespan { get; set; } // in years

       [BsonElement("size")]
    public string? Size { get; set; }

    [BsonElement("species")]
    public string? Species { get; set; }

   [BsonElement("status")]
    public string? Status { get; set; }
    

}