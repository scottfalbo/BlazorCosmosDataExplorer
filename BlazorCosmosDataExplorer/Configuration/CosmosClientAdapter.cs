// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using Microsoft.Azure.Cosmos;

namespace BlazorCosmosDataExplorer.Configuration;

public class CosmosClientAdapter : ICosmosClientAdapter
{
    private readonly CosmosClient _cosmosClient;

    public CosmosClientAdapter(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
    }
}