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

    private string Query { get; set; }

    private List<DomainModel> Results { get; set; }

    private async Task ProcessQuery()
    {
        Results = await DataExplorerProcessor.Process(Query);
    }
}

#nullable enable