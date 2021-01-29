namespace Zatoichi.MongoDb
{
    using ChaosMonkey.Guards;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class MongoDbIoc
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            Guard.IsNotNull(services, nameof(services));
            Guard.IsNotNull(configuration, nameof(configuration));

            services.Configure<MongoOptions>(configuration.GetSection("MongoOptions"))
                .AddSingleton<IMongoClientFactory, MongoClientFactory>()
                .AddSingleton<IMongoClientSettingsWrapper, MongoClientSettingsWrapper>()
                .AddSingleton<IMongoIdentityFactory, MongoIdentityFactory>()
                .AddSingleton<IMongoRepository, MongoRepository>();

            return services;
        }
    }
}