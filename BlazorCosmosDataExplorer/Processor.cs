// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using BlazorCosmosDataExplorer.Models;
using Microsoft.JSInterop;
using System.Dynamic;

namespace BlazorCosmosDataExplorer;

public class Processor : IProcessor
{
    private readonly IRepository _dataExplorerRepository;

    private readonly IExcelWorkbookFactory _excelWorkbookFactory;

    private readonly IJSRuntime _jsRuntime;

    public Processor(
        IRepository dataExplorerRepository,
        IExcelWorkbookFactory excelWorkbookFactory,
        IJSRuntime jSRuntime)
    {
        _dataExplorerRepository = dataExplorerRepository;
        _excelWorkbookFactory = excelWorkbookFactory;
        _jsRuntime = jSRuntime;
    }

    public async Task DownloadExcel(List<dynamic> domainModels)
    {
        var excelData = _excelWorkbookFactory.Create(domainModels);

        var fileName = $"sample.xlsx";

        var base64 = Convert.ToBase64String(excelData);
        var dataUrl = $"data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,{base64}";

        await _jsRuntime.InvokeVoidAsync("downloadFileFromBase64", fileName, dataUrl);
    }

    public async Task<List<ExpandoObject>> Process(QueryInput queryInput)
    {
        var response = await _dataExplorerRepository.GetItems(queryInput);

        var filteredResults = (List<ExpandoObject>)response.FilterProperties();

        return filteredResults;
    }
}