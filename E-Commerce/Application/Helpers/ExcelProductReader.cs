using E_Commerce.Application.DTOs;
using OfficeOpenXml;

namespace E_Commerce.Application.Helpers;

public class ExcelProductReader
{
    public IEnumerable<ExcelProductRowDto> ReadProducts(IFormFile file)
    {
        var rows = new List<ExcelProductRowDto>();
        int rowNumber = 1;

        using var stream = file.OpenReadStream();
        using var package = new ExcelPackage(stream);
        var worksheet = package.Workbook.Worksheets[0];

        for (int row = 2; row <= worksheet.Dimension.Rows; row++)
        {
            rows.Add(new ExcelProductRowDto
            {
                RowNumber = row,
                Name = worksheet.Cells[row, 1].Text,
                Category = worksheet.Cells[row, 2].Text,
                Price = decimal.TryParse(
                    worksheet.Cells[row, 3].Text, out var price) ? price : 0,
                StockQuantity = int.TryParse(
                    worksheet.Cells[row, 4].Text, out var qty) ? qty : -1
            });
        }

        return rows;
    }
}