// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using System.Dynamic;

namespace BlazorCosmosDataExplorer;

public interface IProcessor
{
    Task DownloadExcel(List<dynamic> domainModels);

    Task<List<ExpandoObject>> Process(QueryInput query);
}