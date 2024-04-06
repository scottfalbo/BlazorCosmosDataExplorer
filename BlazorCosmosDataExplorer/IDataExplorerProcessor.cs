// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;

namespace BlazorCosmosDataExplorer;

public interface IDataExplorerProcessor
{
    Task DownloadExcel(List<object> domainModels);

    Task<List<object>> Process(QueryInput query);
}