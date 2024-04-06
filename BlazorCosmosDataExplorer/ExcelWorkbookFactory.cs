// ------------------------------------
// Cosmos Data Explorer
// ------------------------------------

using ClosedXML.Excel;

namespace BlazorCosmosDataExplorer;

public class ExcelWorkbookFactory : IExcelWorkbookFactory
{
    public byte[] Create(List<object> domainModels)
    {
        using var workbook = new XLWorkbook();

        AddWorksheet(workbook, "Sample_Data", domainModels);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    private static void AddWorksheet<T>(XLWorkbook workbook, string sheetName, IEnumerable<T> data)
    {
        var sheet = workbook.Worksheets.Add(sheetName);
        sheet.Cell(1, 1).InsertTable(data.AsEnumerable(), $"{sheetName}_Table", true);
        sheet.Columns().AdjustToContents();
    }
}