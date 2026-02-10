namespace E_Commerce.Application.DTOs;

public class ExcelUploadResultDto
{
    public int InsertedCount { get; set; }
    public List<string> Errors { get; set; }
        = new();
}