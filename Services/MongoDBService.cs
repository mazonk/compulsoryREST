using SubMan.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace CompulsoryREST.Services
{
    public class MongoDBService {
        private readonly IMongoDatabase _database;
