// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;

namespace BlazorCosmosDataExplorer;

public interface IDataExplorerRepository
{
    Task<List<DomainModel>> GetItems(string query);
}