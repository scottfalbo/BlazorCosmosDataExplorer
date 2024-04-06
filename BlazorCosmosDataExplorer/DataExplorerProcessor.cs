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

    public async Task<List<DomainModel>> Process(QueryInput queryInput)
    {
        var response = await _dataExplorerRepository.GetItems(queryInput);
        return response;
    }
}