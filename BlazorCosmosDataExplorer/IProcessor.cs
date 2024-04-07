// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using System.Dynamic;

namespace BlazorCosmosDataExplorer;

public interface IProcessor
{
    Task DownloadExcel(List<ExpandoObject> domainModels);

    Task<List<ExpandoObject>> Process(QueryInput query);
}