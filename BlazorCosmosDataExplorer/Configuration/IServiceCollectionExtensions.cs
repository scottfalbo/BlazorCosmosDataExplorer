// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;
using Microsoft.Azure.Cosmos;

namespace BlazorCosmosDataExplorer.Configuration;

public static class IServiceCollectionExtensions
{
    public static async Task AddDatabaseLookup(this IServiceCollection serviceCollection, CosmosClient cosmosClient)
    {
        var databaseLookup = new Dictionary<string, IContainerLookup>();

        var iterator = cosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();
        var databases = await iterator.ReadNextAsync();

        foreach (var database in databases)
        {
            var containerLookup = new Dictionary<string, IReadOnlyList<string>>();

            var containerIterator = cosmosClient.GetDatabase(database.Id).GetContainerQueryIterator<ContainerProperties>();
            var containers = await containerIterator.ReadNextAsync();

            foreach (var container in containers)
            {
                var containerClient = cosmosClient.GetContainer(database.Id, container.Id);
                var containerProperties = await containerClient.ReadContainerAsync();

                var indexedProperties = containerProperties.Resource.IndexingPolicy.IncludedPaths
                    .SelectMany(path => path.Path.TrimStart('/').Split('/'))
                    .Distinct()
                    .ToList();

                containerLookup.Add(container.Id, indexedProperties);
            }

            databaseLookup.Add(database.Id, new ContainerLookup(containerLookup));
        }

        var databaseLookupService = new DatabaseLookup(databaseLookup);
        serviceCollection.AddSingleton<IDatabaseLookup>(databaseLookupService);
    }
}