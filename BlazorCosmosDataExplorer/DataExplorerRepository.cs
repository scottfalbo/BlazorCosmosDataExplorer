// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace BlazorCosmosDataExplorer;

public class DataExplorerRepository : IDataExplorerRepository
{
    private readonly IDataExplorerClient _dataExplorerClient;
    private readonly DataExplorerMapper _mapper = new();

    public DataExplorerRepository(IDataExplorerClient dataExplorerClient)
    {
        _dataExplorerClient = dataExplorerClient;
    }

    public async Task<List<DomainModel>> GetItems(QueryInput queryInput)
    {
        var scryer = _dataExplorerClient.CreateClient();
        var container = scryer.GetContainer(queryInput.Database, queryInput.Container);

        var domainModels = new List<DomainModel>();

        try
        {
            var query = container.GetItemQueryIterator<StorageContract>(queryInput.Query);

            while (query.HasMoreResults)
            {
                var results = await query.ReadNextAsync();

                foreach (var storageContact in results)
                {
                    var domainModel = _mapper.ToDomainModel(storageContact);
                    domainModels.Add(domainModel);
                }
            }

            return domainModels;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return new List<DomainModel>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}