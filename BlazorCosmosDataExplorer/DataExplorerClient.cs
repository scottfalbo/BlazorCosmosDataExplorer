// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using Microsoft.Azure.Cosmos;

namespace BlazorCosmosDataExplorer;

public class DataExplorerClient : IDataExplorerClient
{
    private readonly CosmosClient _cosmosClient;

    public DataExplorerClient(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
    }

    public CosmosClient CreateClient() => _cosmosClient;
}