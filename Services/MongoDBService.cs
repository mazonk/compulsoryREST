using SubMan.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace CompulsoryREST.Services
{
    public class MongoDBService
    {
        private readonly IMongoDatabase _database;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            Console.WriteLine("Connecting to MongoDB...");
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            _database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        }

        // This method allows you to get any collection from the database, based on the collection name you pass
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        public async Task CreateAsync(Subscription subscription)
        {
            var subscriptionCollection = GetCollection<Subscription>("subscriptions"); // Or specify the collection name
            await subscriptionCollection.InsertOneAsync(subscription);
        }

        public async Task<List<Subscription>> GetAsync()
        {
            var subscriptionCollection = GetCollection<Subscription>("subscriptions"); // Or specify the collection name
            return await subscriptionCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateAsync(string id, Subscription subscription)
        {
            var subscriptionCollection = GetCollection<Subscription>("subscriptions"); // Or specify the collection name
            FilterDefinition<Subscription> filter = Builders<Subscription>.Filter.Eq("Id", id);
            UpdateDefinition<Subscription> update = Builders<Subscription>.Update
                .Set("name", subscription.Name)
                .Set("location", subscription.Location)
                .Set("lifespan", subscription.Lifespan)
                .Set("size", subscription.Size)
                .Set("description", subscription.Description);

            await subscriptionCollection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteAsync(string id)
        {
            var subscriptionCollection = GetCollection<Subscription>("subscriptions"); // Or specify the collection name
            FilterDefinition<Subscription> filter = Builders<Subscription>.Filter.Eq("Id", id);
            await subscriptionCollection.DeleteOneAsync(filter);
        }
    }
}
