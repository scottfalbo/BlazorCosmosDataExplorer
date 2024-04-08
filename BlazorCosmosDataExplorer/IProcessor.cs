// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using System.Dynamic;

namespace BlazorCosmosDataExplorer;

public interface IProcessor
{
    Task DownloadExcel(List<dynamic> results, List<dynamic> filteredResults);

    Task<Dictionary<string, List<string>>> GetDatabasesAndContainers();

    Task<List<ExpandoObject>> Process(QueryInput query);
}