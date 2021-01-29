namespace Zatoichi.MongoDb
{
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public class MongoIdentityFactory : IMongoIdentityFactory
    {
        // ReSharper disable once InconsistentNaming
        public const string ADMIN = "admin";

        private readonly IOptions<MongoOptions> options;

        public MongoIdentityFactory(IOptions<MongoOptions> options)
        {
            this.options = options;
        }

        public MongoCredential CreateIdentity()
        {
            this.InternalIdentity = new MongoInternalIdentity(ADMIN, this.options.Value.Username);
            this.PasswordEvidence = new PasswordEvidence(this.options.Value.Password);
            return new MongoCredential(this.options.Value.AuthMechanism, this.InternalIdentity, this.PasswordEvidence);
        }

        internal PasswordEvidence PasswordEvidence { get; private set; }

        internal MongoInternalIdentity InternalIdentity { get; private set; }
    }
}