// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace BlazorCosmosDataExplorer;

public class DataExplorerRepository : IDataExplorerRepository
{
    public DataExplorerRepository(IDataExplorerClient dataExplorerClient)
    {
        _dataExplorerClient = dataExplorerClient;
    }

    public async Task<List<object>> GetItems(QueryInput queryInput)
    {
        var client = _dataExplorerClient.CreateClient();
        var container = client.GetContainer(queryInput.Database, queryInput.Container);

        var domainModels = new List<object>();

        try
        {
            var queryDefinition = new QueryDefinition(queryInput.Query);

            using var resultIterator = container.GetItemQueryIterator<object>(queryDefinition);

            while (resultIterator.HasMoreResults)
            {
                var storageContracts = await resultIterator.ReadNextAsync();
                domainModels.AddRange(storageContracts.Select(x => _mapper.ToDomainModel(x)));
            }

            return domainModels;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return new List<object>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    private readonly IDataExplorerClient _dataExplorerClient;
    private readonly DataExplorerMapper _mapper = new();
}