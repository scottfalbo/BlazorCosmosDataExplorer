// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using Azure;
using BlazorCosmosDataExplorer.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;
using System.ComponentModel;
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
            //var container = GetContainer(ContainerId, serviceContext);
            //var queryString = "SELECT * FROM LedgerEntry e WHERE e.partitionKey = @partitionKey AND e.operationId = @operationId";

            //if (!includeReverted)
            //{
            //    queryString += " AND NOT IS_DEFINED(e.revertedOn)";
            //}

            //var queryDefinition = new QueryDefinition(queryString)
            //    .WithParameter("@partitionKey", partitionKey)
            //    .WithParameter("@operationId", operationId);

            //using var resultIterator = container.GetItemQueryIterator<LedgerEntryStorageContractV2>(queryDefinition);

            //var list = new List<LedgerEntryStorageContractV2>();
            //while (resultIterator.HasMoreResults)
            //{
            //    var currentSet = await resultIterator.ReadNextAsync(serviceContext.IsTest());
            //    list.AddRange
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return new List<DomainModel>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }

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