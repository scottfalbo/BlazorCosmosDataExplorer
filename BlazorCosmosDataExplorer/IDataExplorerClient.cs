// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using Microsoft.Azure.Cosmos;

namespace BlazorCosmosDataExplorer;

public interface IDataExplorerClient
{
    CosmosClient CreateClient();
}