// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;

namespace BlazorCosmosDataExplorer;

public interface IDataExplorerProcessor
{
    Task<List<DomainModel>> Process(string query);
}