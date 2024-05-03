// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using Microsoft.Azure.Cosmos;
using System.Dynamic;

namespace BlazorCosmosDataExplorer.Models;

public static class Extensions
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

    public static IEnumerable<dynamic> FilterProperties(this IEnumerable<dynamic> items)
    {
        var filteredItems = new List<ExpandoObject>();

        foreach (var item in items)
        {
            IDictionary<string, object?> expandoAsDictionary = new ExpandoObject();

            foreach (var kvp in (IDictionary<string, object>)item)
            {
                if (!kvp.Key.StartsWith("_"))
                {
                    expandoAsDictionary[kvp.Key] = kvp.Value;
                }
            }

            filteredItems.Add((ExpandoObject)expandoAsDictionary);
        }

        return filteredItems;
    }

    public static List<dynamic> SortResults(this List<dynamic> results, string columnName, bool ascending)
    {
        return ascending
            ? results.OrderBy(x => ((IDictionary<string, object>)x)[columnName]).ToList()
            : results.OrderByDescending(x => ((IDictionary<string, object>)x)[columnName]).ToList();
    }

    //public static async Task AddCosmosClient(this IServiceCollection serviceCollection, CosmosAccountConfiguration configuration)
    //{
    //}
}