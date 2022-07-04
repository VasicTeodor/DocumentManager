using ITCompanyCVManager.Persistence.ElasticSearchIndexSettings;
using Nest;

namespace ITCompanyCVManager.Api.Configuration;

public static class ElasticSearchConfiguration
{
    public static void ConfigureElasticSearch(this IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration["elasticsearch:url"];
        var defaultIndex = configuration["elasticsearch:index"];

        var settings = new ConnectionSettings(new Uri(url))
            .DefaultIndex(defaultIndex)
            .EnableDebugMode();

        ElasticSearchMappings.DefaultMappings(settings);

        var client = new ElasticClient(settings);

        services.AddSingleton<IElasticClient>(client);

        ElasticSearchMappings.CreateApplicationIndex(client, defaultIndex);
    }
}