using E_Commerce.Application.Interfaces.Services;
using E_Commerce.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers.Admin;

// [Area("Admin")] 
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
        var result = await _service.GetPagedAsync(
            pageNumber, pageSize, search, category);

        var vm = result.ToListViewModel();
        vm.Search = search;
        vm.Category = category;

        return View(vm.Items);
    }

    public async Task<IActionResult> Create(CreateProductViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _service.AddProductAsync(model.ToDto());
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _service.GetByIdAsync(id);
        return View(product.ToEditViewModel());
    }
    
    [HttpGet]
    public IActionResult UploadExcel()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UploadExcel(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ModelState.AddModelError("", "Please upload a valid Excel file.");
            return View();
        }

        var rows = _excelReader.ReadProducts(file); // helper
        var result = await _productService.UploadFromExcelAsync(rows);

        ViewBag.InsertedCount = result.InsertedCount;
        ViewBag.Errors = result.Errors;

        return View();
    }

    
    
}