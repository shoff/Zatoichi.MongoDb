namespace Zatoichi.MongoDb
{
    using MongoDB.Driver;

    public interface IMongoIdentityFactory
    {
        MongoCredential CreateIdentity();
    }
}