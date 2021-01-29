namespace Zatoichi.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Core.Configuration;

    public interface IMongoClientSettingsWrapper
    {
        bool UseConnectionString { get; set; }
        MongoClientSettings Clone();
        bool Equals(MongoClientSettings obj);
        bool Equals(object obj);
        MongoClientSettings Freeze();
        MongoClientSettings FrozenCopy();
        int GetHashCode();
        string ToString();
        bool AllowInsecureTls { get; set; }
        string ApplicationName { get; set; }
        IReadOnlyList<CompressorConfiguration> Compressors { get; set; }
        Action<ClusterBuilder> ClusterConfigurator { get; set; }
        ConnectionMode ConnectionMode { get; set; }
        TimeSpan ConnectTimeout { get; set; }
        MongoCredential Credential { get; set; }
        IEnumerable<MongoCredential> Credentials { get; set; }
        GuidRepresentation GuidRepresentation { get; set; }
        bool IsFrozen { get; }
        TimeSpan HeartbeatInterval { get; set; }
        TimeSpan HeartbeatTimeout { get; set; }
        bool IPv6 { get; set; }
        TimeSpan LocalThreshold { get; set; }
        TimeSpan MaxConnectionIdleTime { get; set; }
        TimeSpan MaxConnectionLifeTime { get; set; }
        int MaxConnectionPoolSize { get; set; }
        int MinConnectionPoolSize { get; set; }
        ReadConcern ReadConcern { get; set; }
        UTF8Encoding ReadEncoding { get; set; }
        ReadPreference ReadPreference { get; set; }
        string ReplicaSetName { get; set; }
        bool RetryReads { get; set; }
        bool RetryWrites { get; set; }
        ConnectionStringScheme Scheme { get; set; }
        string SdamLogFilename { get; set; }
        MongoServerAddress Server { get; set; }
        IEnumerable<MongoServerAddress> Servers { get; set; }
        TimeSpan ServerSelectionTimeout { get; set; }
        TimeSpan SocketTimeout { get; set; }
        SslSettings SslSettings { get; set; }
        bool UseSsl { get; set; }
        bool UseTls { get; set; }
        bool VerifySslCertificate { get; set; }
        int WaitQueueSize { get; set; }
        TimeSpan WaitQueueTimeout { get; set; }
        WriteConcern WriteConcern { get; set; }
        UTF8Encoding WriteEncoding { get; set; }
    }
}