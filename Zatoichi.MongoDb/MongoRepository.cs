// ReSharper disable InconsistentNaming
namespace Zatoichi.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using ChaosMonkey.Guards;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class MongoRepository : IMongoRepository
    {
        protected IMongoDatabase mongoDatabase;
        private readonly IMongoClient client;

        public MongoRepository(
            IOptions<MongoOptions> options,
            IMongoClientFactory mongoClientFactory)
        {
            Guard.IsNotNull(options, nameof(options));
            Guard.IsNotNullOrWhitespace(options.Value.Password, nameof(options.Value.Password));
            Guard.IsNotNullOrWhitespace(options.Value.Username, nameof(options.Value.Username));
            Guard.IsNotNull(mongoClientFactory, nameof(mongoClientFactory));
            this.client = mongoClientFactory.CreateClient(options.Value.ToString());
            this.mongoDatabase = this.client.GetDatabase(options.Value.DefaultDb);
        }

        public IQueryable<T> All<T>(string collectionName) where T : class, new()
        {
            Guard.IsNotNullOrWhitespace(collectionName, nameof(collectionName));
            return this.mongoDatabase.GetCollection<T>(collectionName)
                .AsQueryable(new AggregateOptions { AllowDiskUse = true });
        }

        public IQueryable<T> Where<T>(Expression<Func<T, bool>> expression, string collectionName)
            where T : class, new()
        {
            Guard.IsNotNullOrWhitespace(collectionName, nameof(collectionName));
            Guard.IsNotNull(expression, nameof(expression));
            return this.All<T>(collectionName).Where(expression);
        }

        public void Delete<T>(Expression<Func<T, bool>> expression, string collectionName) where T : class, new()
        {
            Guard.IsNotNullOrWhitespace(collectionName, nameof(collectionName));
            Guard.IsNotNull(expression, nameof(expression));
            this.mongoDatabase.GetCollection<T>(collectionName).DeleteMany(expression);
        }

        public T Single<T>(Expression<Func<T, bool>> expression, string collectionName) where T : class, new()
        {
            Guard.IsNotNullOrWhitespace(collectionName, nameof(collectionName));
            Guard.IsNotNull(expression, nameof(expression));
            var queryable = this.All<T>(collectionName);
            queryable = queryable.Where(expression);
            return queryable.SingleOrDefault();
        }

        public bool CollectionExists<T>(string collectionName,
            CountOptions options = null,
            MongoCollectionSettings mongoCollectionSettings = null,
            CancellationToken cancellationToken = default)
            where T : class, new()
        {
            Guard.IsNotNullOrWhitespace(collectionName, nameof(collectionName));

            var collection = this.mongoDatabase.GetCollection<T>(collectionName, mongoCollectionSettings);
            var filter = new BsonDocument();
            var totalCount = collection.CountDocuments(filter, options, cancellationToken);
            return totalCount > 0;
        }

        public void Add<T>(T item, string collectionName) where T : class, new()
        {
            Guard.IsNotNullOrWhitespace(collectionName, nameof(collectionName));
            Guard.IsNotDefault(item, nameof(item));
            var collection = this.mongoDatabase.GetCollection<T>(collectionName);
            collection.InsertOne(item);
        }

        public void Add<T>(IEnumerable<T> items, string collectionName) where T : class, new()
        {
            Guard.IsNotNullOrWhitespace(collectionName, nameof(collectionName));
            // ReSharper disable PossibleMultipleEnumeration
            Guard.IsNotNullOrEmpty(items, nameof(items));
            var collection = this.mongoDatabase.GetCollection<T>(collectionName);
            collection.InsertMany(items);
            // ReSharper restore PossibleMultipleEnumeration
        }

        public IMongoDatabase Database => this.mongoDatabase;
    }
}