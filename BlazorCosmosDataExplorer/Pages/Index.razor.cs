// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorCosmosDataExplorer.Pages;

#nullable disable

public partial class Index : ComponentBase
{
    [Inject]
    private IDataExplorerProcessor _dataExplorerProcessor { get; set; }

    private bool AppendResults { get; set; } = false;

    private string Container { get; set; } = "ContainerOne";

    private string CurrentSortColumn { get; set; }

    private string Database { get; set; } = "PaleSpecter";

    private bool IsSortAscending { get; set; } = true;

    [Inject]
    private IJSRuntime JSRuntime { get; set; }

    private string Query { get; set; } = "select * from c where c.partitionKey = \"partitionKey_01\"";

    private List<DomainModel> Results { get; set; } = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("preventShiftEnter", "query_field");
        }
    }

    private void DeDupe()
    {
        var deDupedResults = Results.GroupBy(x => x.Id).Select(x => x.First()).ToList();
        Results.Clear();
        Results.AddRange(deDupedResults);
    }

    private void DownloadExcel()
    {
        if (Results.Count > 0)
        {
            _dataExplorerProcessor.DownloadExcel(Results);
        }
    }

    private Task HandleFormSubmit()
    {
        // Prevent the form from submitting when the Enter key is pressed without Shift.
        // This is called on form submission, which we don't want to happen on just Enter.
        return Task.CompletedTask;
    }

    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Enter" && e.ShiftKey)
        {
            await ProcessQuery();
        }
    }

    private async Task ProcessQuery()
    {
        if (!AppendResults)
        {
            Results.Clear();
        }

        var queryInput = new QueryInput(Query, Container, Database);
        var results = await _dataExplorerProcessor.Process(queryInput);

        Results.AddRange(results);
        DeDupe();
    }

    private List<DomainModel> SortResults(List<DomainModel> results, string columnName, bool ascending)
    {
        var propertyInfo = typeof(DomainModel).GetProperty(columnName);
        if (propertyInfo == null) return results;

        return ascending
            ? results.OrderBy(e => propertyInfo.GetValue(e, null)).ToList()
            : results.OrderByDescending(e => propertyInfo.GetValue(e, null)).ToList();
    }

    private void SortTable(string columnName)
    {
        if (columnName == CurrentSortColumn)
        {
            IsSortAscending = !IsSortAscending;
        }
        else
        {
            CurrentSortColumn = columnName;
            IsSortAscending = true;
        }

        Results = SortResults(Results!, CurrentSortColumn, IsSortAscending);
    }
}

#nullable enable