using MongoDB.Driver;

namespace uBeac.Core.Repositories.MongoDB
{
    public interface IMongoDBContext
    {
        IMongoDatabase Database { get; }
    }

    public class MainDBContext : IMongoDBContext
    {
        public MainDBContext(MongoDBOptions<MainDBContext> option)
        {
            var mongoUrl = new MongoUrl(option.ConnectionString);
            var client = new MongoClient(mongoUrl);
            Database = client.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase Database { get; }
    }
}
