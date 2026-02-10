using E_Commerce.Application.Interfaces.Services;
using E_Commerce.Models.Customer.Product;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers.Customer;


[Area("Customer")]
public class ProductsController : Controller
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(
        int pageNumber = 1, 
        int pageSize = 10,
        string? search = null, 
        string? category = null)
    {
        var result = await _service.GetPagedAsync(pageNumber, pageSize, search, category);
        
        var vm = result.ToCustomerListViewModel(pageNumber, pageSize);
        vm.Search = search;
        vm.Category = category;

        return View(vm);
    }
}
