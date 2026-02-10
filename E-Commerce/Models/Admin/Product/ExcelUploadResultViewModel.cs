namespace E_Commerce.Models.Admin.Product;

public class ExcelUploadResultViewModel
{

    
    public int InsertedCount { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    public bool HasErrors => Errors.Any();
}
