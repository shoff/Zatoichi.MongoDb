namespace Zatoichi.MongoDb
{
    using MongoDB.Driver;

    public interface IMongoClientFactory
    {
        IMongoClient CreateClient();
        IMongoClient CreateClient(string connectionString);
    }
}