// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;

namespace BlazorCosmosDataExplorer;

public interface IExcelWorkbookFactory
{
    byte[] Create(List<DomainModel> domainModels);
}