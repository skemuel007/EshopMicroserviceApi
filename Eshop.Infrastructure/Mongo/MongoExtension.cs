using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Eshop.Infrastructure.Mongo;

// https://medium.com/@jaydeepvpatil225/mongodb-basics-and-crud-operation-using-net-core-7-web-api-884b5b76549a
public static class MongoExtension
{
    public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var configSection = configuration.GetSection("Mongo");
        var mongoConfig = new MongoConfig();
        configSection.Bind(mongoConfig);

        services.AddSingleton<IMongoClient>(client => new MongoClient(mongoConfig.ConnectionString));

        services.AddSingleton<IMongoDatabase>(client =>
        {
            var mongoClient = client.GetService<IMongoClient>();
            return mongoClient.GetDatabase(mongoConfig.Database);
        });

        services.AddSingleton<IDatabaseInitializer, MongoInitializer>();
    }
}