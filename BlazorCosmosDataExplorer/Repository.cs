// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using Microsoft.Azure.Cosmos;
using System.Dynamic;
using System.Net;

namespace BlazorCosmosDataExplorer;

public class Repository : IRepository
{
    private readonly IClient _dataExplorerClient;

    public Repository(IClient dataExplorerClient)
    {
        _dataExplorerClient = dataExplorerClient;
    }

    public async Task<List<ExpandoObject>> GetItems(QueryInput queryInput)
    {
        var client = _dataExplorerClient.CreateClient();
        var container = client.GetContainer(queryInput.Database, queryInput.Container);

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