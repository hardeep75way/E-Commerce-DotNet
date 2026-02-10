namespace E_Commerce.Models.Admin.Product;

public class ProductListViewModel
{
    
    public IEnumerable<ProductListItemViewModel> Items { get; set; }
        = Enumerable.Empty<ProductListItemViewModel>();

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public int TotalPages =>
        (int)Math.Ceiling((double)TotalCount / PageSize);

    public string? Search { get; set; }
    public string? Category { get; set; }

}