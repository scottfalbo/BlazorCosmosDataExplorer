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

        foreach (var db in databases)
        {
            var containersList = new List<string>();
            Database databaseRef = _client.GetDatabase(db.Id);
            var containerIterator = databaseRef.GetContainerQueryIterator<ContainerProperties>();
            do
            {
                var containers = await containerIterator.ReadNextAsync();
                foreach (var container in containers)
                {
                    containersList.Add(container.Id);
                }
            } while (containerIterator.HasMoreResults);

            databasesAndContainers.Add(db.Id, containersList);
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

            while (resultIterator.HasMoreResults)
            {
                var storageResults = await resultIterator.ReadNextAsync();
                results.AddRange(storageResults);
            }

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