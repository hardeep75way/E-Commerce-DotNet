namespace E_Commerce.Models.Customer.Product;

public class CustomerProductListViewModel
{
    public IEnumerable<CustomerProductItemViewModel> Products { get; set; }
        = Enumerable.Empty<CustomerProductItemViewModel>();
    
    public string? Search { get; set; }
    public string? Category { get; set; }
    public int TotalCount { get; set; }
    
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
