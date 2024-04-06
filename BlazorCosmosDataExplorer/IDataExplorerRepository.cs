// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;

namespace BlazorCosmosDataExplorer;

public interface IDataExplorerRepository
{
    Task<List<object>> GetItems(QueryInput queryInput);
}