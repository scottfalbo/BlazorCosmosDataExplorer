// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;
using Microsoft.Azure.Cosmos;
using System.Dynamic;
using System.Net;

namespace BlazorCosmosDataExplorer;

public class Repository : IRepository
{
    private readonly CosmosClient _cosmosClient;

    public Repository(IClient client)
    {
        _cosmosClient = client.CreateClient();
    }

    public async Task<List<ExpandoObject>> GetItems(QueryInput queryInput)
    {
        var container = _cosmosClient.GetContainer(queryInput.Database, queryInput.Container);

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