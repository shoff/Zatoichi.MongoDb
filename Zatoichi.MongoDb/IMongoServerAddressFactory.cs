namespace Zatoichi.MongoDb
{
    using MongoDB.Driver;

    public interface IMongoServerAddressFactory
    {
        MongoServerAddress CreateServer();
    }
}