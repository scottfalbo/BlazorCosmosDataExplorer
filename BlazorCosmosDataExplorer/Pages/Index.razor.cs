// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorCosmosDataExplorer.Pages;

#nullable disable

public partial class Index : ComponentBase
{
    [Inject]
    private IDataExplorerProcessor _dataExplorerProcessor { get; set; }

    private string Container { get; set; } = "ContainerOne";

    private string Database { get; set; } = "PaleSpecter";

    private string Query { get; set; }

    private List<DomainModel> Results { get; set; } = new();

    private void DownloadExcel()
    {
        if (Results.Count > 0)
        {
            _dataExplorerProcessor.DownloadExcel(Results);
        }
    }

    private async Task ProcessQuery()
    {
        var queryInput = new QueryInput(Query, Container, Database);
        var results = await _dataExplorerProcessor.Process(queryInput);
        Results = results;
    }
}

#nullable enable