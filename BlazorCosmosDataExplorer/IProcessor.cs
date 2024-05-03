// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using System.Dynamic;
using BlazorCosmosDataExplorer.Models;

namespace BlazorCosmosDataExplorer;

public interface IProcessor
{
    Task DownloadExcel(List<dynamic> results, List<dynamic> filteredResults);

    IDatabaseLookup DatabaseLookup();

    Task<List<ExpandoObject>> Process(QueryInput query);
}