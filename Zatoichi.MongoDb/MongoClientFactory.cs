namespace Zatoichi.MongoDb
{
    using MongoDB.Driver;

    public class MongoClientFactory : IMongoClientFactory
    {
        private readonly IMongoClientSettingsWrapper clientSettings;

        public MongoClientFactory(IMongoClientSettingsWrapper clientSettings)
        {
            this.clientSettings = clientSettings;
        }
        
        public IMongoClient CreateClient()
        {
            return new MongoClient(this.clientSettings.Clone());
        }

        public IMongoClient CreateClient(string connectionString)
        {
            if (this.clientSettings.UseConnectionString)
            {
                return new MongoClient(
                    MongoClientSettings.FromConnectionString
                        (connectionString));
            }

            return this.CreateClient();
        }
    }
}