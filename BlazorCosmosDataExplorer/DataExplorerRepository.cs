// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;

namespace BlazorCosmosDataExplorer;

public class DataExplorerRepository : IDataExplorerRepository
{
    private readonly DataExplorerMapper _mapper = new();

    public async Task<List<DomainModel>> GetItems(string query)
    {
        await Task.CompletedTask;
        return new List<DomainModel>();
    }
}