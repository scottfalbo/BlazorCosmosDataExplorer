// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;

namespace BlazorCosmosDataExplorer;

public class DataExplorerProcessor : IDataExplorerProcessor
{
    public async Task<List<DomainModel>> Process(string query)
    {
        await Task.CompletedTask;
        return new List<DomainModel>();
    }
}