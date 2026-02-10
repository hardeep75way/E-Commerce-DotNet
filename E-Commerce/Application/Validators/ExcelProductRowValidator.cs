using E_Commerce.Application.DTOs;

namespace E_Commerce.Application.Validators;

public class ExcelProductRowValidator
{
    public static bool IsValid(
        ExcelProductRowDto row,
        out string error)
    {
        error = string.Empty;

        if (string.IsNullOrWhiteSpace(row.Name))
        {
            error = "Product name is required.";
            return false;
        }

        if (row.Price <= 0)
        {
            error = "Price must be greater than zero.";
            return false;
        }

        if (row.StockQuantity < 0)
        {
            error = "Stock cannot be negative.";
            return false;
        }

        return true;
    }
}