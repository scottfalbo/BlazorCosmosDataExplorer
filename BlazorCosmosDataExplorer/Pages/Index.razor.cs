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
    public IDataExplorerProcessor DataExplorerProcessor { get; set; }

    private string Container { get; set; } = "ContainerOne";

    private string Database { get; set; } = "PaleSpecter";

    private string Query { get; set; }

    private List<DomainModel> Results { get; set; }

    private async Task ProcessQuery()
    {
        var queryInput = new QueryInput(Query, Container, Database);
        var results = await DataExplorerProcessor.Process(queryInput);
        Results = results;
    }
}

#nullable enable