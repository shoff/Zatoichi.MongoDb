namespace Zatoichi.MongoDb
{
    using System;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    /// <summary>
    /// Simple wrapper class to allow us to pass in a settings object to the mongo repository
    /// </summary>
    public class MongoClientSettingsWrapper : MongoClientSettings, IMongoClientSettingsWrapper
    {
        public MongoClientSettingsWrapper(
            IOptions<MongoOptions> options,
            IMongoIdentityFactory mongoIdentityFactory)
        {
            this.Credential = mongoIdentityFactory.CreateIdentity();
            this.GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard;

            if (string.IsNullOrWhiteSpace(options.Value.ReplicaSetName))
            {
                if (string.IsNullOrWhiteSpace(options.Value.MongoHost))
                {
                    throw new ApplicationException("Either specify a replica set in the MongoOptions or a single MongoHost.");
                }

                if (options.Value.Port == 0)
                {
                    throw new ApplicationException("When using a single host the Port property must be set to a value greater than 0. Usually 27017.");
                }
                this.Server = new MongoServerAddress(options.Value.MongoHost, options.Value.Port);
            }
            else
            {
                this.ReplicaSetName = options.Value.ReplicaSetName;
                this.UseConnectionString = true;
            }
        }

        public bool UseConnectionString { get; set; }
    }
}