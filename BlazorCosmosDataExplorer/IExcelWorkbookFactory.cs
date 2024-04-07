// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using System.Dynamic;

namespace BlazorCosmosDataExplorer;

public interface IExcelWorkbookFactory
{
    byte[] Create(List<ExpandoObject> domainModels);
}