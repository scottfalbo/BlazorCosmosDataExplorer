// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using Microsoft.Azure.Cosmos;

namespace BlazorCosmosDataExplorer;

public class Client : IClient
{
    private readonly CosmosClient _cosmosClient;

    public Client(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
    }

    public CosmosClient CreateClient() => _cosmosClient;
}