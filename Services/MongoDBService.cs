using CompulsoryREST.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace CompulsoryREST.Services{
    public class MongoDBService {
    private readonly IMongoCollection<Creature> _creaturesCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings) {
        Console.WriteLine($"Connecting to MongoDB)");
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _creaturesCollection = database.GetCollection<Creature>(mongoDBSettings.Value.CollectionName);
    }

    public async Task CreateAsync(Creature creature ) {
        await _creaturesCollection.InsertOneAsync(creature);
        return;
    }
    public async Task<List<Creature>> GetAsync() {
        return await _creaturesCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateAsync(string id, Creature creature) {
        FilterDefinition<Creature> filter = Builders<Creature>.Filter.Eq("Id", id);
        UpdateDefinition<Creature> update = Builders<Creature>.Update
            .Set("name", creature.Name)
            .Set("species", creature.Species)
            .Set("location", creature.Location)
            .Set("lifespan", creature.Lifespan)
            .Set("size", creature.Size)
            .Set("status", creature.Status)
            .Set("description", creature.Description);
        
        await _creaturesCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteAsync(string id) {
        FilterDefinition<Creature> filter = Builders<Creature>.Filter.Eq("Id", id);
        await _creaturesCollection.DeleteOneAsync(filter);
        return;
    }
    }
}