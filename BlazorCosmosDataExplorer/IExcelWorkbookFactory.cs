// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

namespace BlazorCosmosDataExplorer;

public interface IExcelWorkbookFactory
{
    byte[] Create(List<dynamic> domainModels);
}