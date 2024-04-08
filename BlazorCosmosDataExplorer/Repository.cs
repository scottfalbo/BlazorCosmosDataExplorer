// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using Microsoft.Azure.Cosmos;
using System.Dynamic;
using System.Net;

namespace BlazorCosmosDataExplorer;

public class Repository : IRepository
{
    private readonly CosmosClient _client;

    public Repository(IClient client)
    {
        _client = client.CreateClient();
    }

    public async Task<Dictionary<string, List<string>>> GetDatabasesAndContainers()
    {
        var databasesAndContainers = new Dictionary<string, List<string>>();

        var iterator = _client.GetDatabaseQueryIterator<DatabaseProperties>();
        var databases = await iterator.ReadNextAsync();

        foreach (var database in databases)
        {
            var containersList = new List<string>();
            var databaseRef = _client.GetDatabase(database.Id);
            var containerIterator = databaseRef.GetContainerQueryIterator<ContainerProperties>();

            while (containerIterator.HasMoreResults)
            {
                var containers = await containerIterator.ReadNextAsync();
                foreach (var container in containers)
                {
                    containersList.Add(container.Id);
                }
            }

            databasesAndContainers.Add(database.Id, containersList);
        }

        return databasesAndContainers;
    }

    public async Task<List<ExpandoObject>> GetItems(QueryInput queryInput)
    {
        var container = _client.GetContainer(queryInput.Database, queryInput.Container);

        try
        {
            var queryDefinition = new QueryDefinition(queryInput.Query);
            var results = new List<ExpandoObject>();

            using var resultIterator = container.GetItemQueryIterator<ExpandoObject>(queryDefinition);

            do
            {
                var storageResults = await resultIterator.ReadNextAsync();
                results.AddRange(storageResults);
            }
            while (resultIterator.HasMoreResults);

            return results;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return new List<ExpandoObject>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}