// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using Microsoft.Azure.Cosmos;

namespace BlazorCosmosDataExplorer.Configuration;

public class CosmosClientAdapter : ICosmosClientAdapter
{
    private readonly CosmosClient _cosmosClient;

    public CosmosClientAdapter(ICosmosClientFactory cosmosClientFactory, CosmosAccountConfiguration cosmosAccountConfiguration)
    {
        _cosmosClient = cosmosClientFactory.CreateCosmosClient(cosmosAccountConfiguration.Endpoint, cosmosAccountConfiguration.Key);
    }
}