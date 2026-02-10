using E_Commerce.Application.DTOs;

namespace E_Commerce.Models.Admin.Product;

public static class ExcelUploadResultExtensions
{
    public static ExcelUploadResultViewModel ToViewModel(this ExcelUploadResultDto dto)
    {
        return new ExcelUploadResultViewModel
        {
            InsertedCount = dto.InsertedCount,
            Errors = dto.Errors.ToList()
        };
    }
}
