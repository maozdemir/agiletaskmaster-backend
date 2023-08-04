using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace AgileTaskMaster.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;
        public IMongoDatabase Database { get; }

        public MongoDBContext(IOptions<MongoDBSettings>? settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>(string name, MongoCollectionSettings settings = null)
        {
            return _database.GetCollection<TDocument>(name, settings);
        }
        public void CreateCollection(string name, CreateCollectionOptions options = null, CancellationToken cancellationToken = default)
        {
            _database.CreateCollection(name, options, cancellationToken);
        }
    }
}
