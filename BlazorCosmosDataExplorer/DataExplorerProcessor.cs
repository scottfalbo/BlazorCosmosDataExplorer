// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;

namespace BlazorCosmosDataExplorer;

public class DataExplorerProcessor : IDataExplorerProcessor
{
    private readonly IDataExplorerRepository _dataExplorerRepository;

    public DataExplorerProcessor(IDataExplorerRepository dataExplorerRepository)
    {
        _dataExplorerRepository = dataExplorerRepository;
    }

    public async Task<List<DomainModel>> Process(string query)
    {
        var response = await _dataExplorerRepository.GetItems(query);
        return response;
    }
}