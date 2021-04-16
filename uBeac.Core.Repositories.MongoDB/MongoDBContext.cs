using MongoDB.Driver;

namespace uBeac.Core.Repositories.MongoDB
{
    public interface IMongoDBContext
    {
        IMongoDatabase Database { get; }
    }

    public class MongoDBContext : IMongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoDatabase Database => _database;

    }
}
