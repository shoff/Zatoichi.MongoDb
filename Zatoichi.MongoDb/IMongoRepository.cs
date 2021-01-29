namespace Zatoichi.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using MongoDB.Driver;

    public interface IMongoRepository
    {
        IMongoDatabase Database { get; }
        IQueryable<T> All<T>(string collectionName) where T : class, new();
        IQueryable<T> Where<T>(Expression<Func<T, bool>> expression, string collectionName) where T : class, new();
        T Single<T>(Expression<Func<T, bool>> expression, string collectionName) where T : class, new();
        void Delete<T>(Expression<Func<T, bool>> expression, string collectionName) where T : class, new();
        void Add<T>(T item, string collectionName) where T : class, new();
        void Add<T>(IEnumerable<T> items, string collectionName) where T : class, new();
        bool CollectionExists<T>(string collectionName, 
            CountOptions options = null,
            MongoCollectionSettings mongoCollectionSettings = null,
            CancellationToken cancellationToken = default) where T : class, new();
    }
}