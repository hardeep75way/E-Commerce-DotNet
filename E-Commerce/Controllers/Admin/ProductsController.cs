using E_Commerce.Application.Helpers;
using E_Commerce.Application.Interfaces.Services;
using E_Commerce.Models.Admin.Product;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers.Admin;


[Area("Admin")]
public class ProductsController : Controller
{
    private readonly IProductService _service;
    private readonly ExcelProductReader _excelReader;

    public ProductsController(IProductService service, ExcelProductReader excelReader)
    {
        _service = service;
        _excelReader = excelReader;
    }

    public async Task<IActionResult> Index(
        int pageNumber = 1,
        int pageSize = 10,
        string? search = null,
        string? category = null)
    {
        var result = await _service.GetPagedAsync(pageNumber, pageSize, search, category);

        var vm = result.ToListViewModel();
        vm.Search = search;
        vm.Category = category;

        return View(vm);
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateProductViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _service.AddProductAsync(model.ToDto());
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _service.GetByIdAsync(id);
        return View(product.ToEditViewModel());
    }

    [HttpPut]
    public async Task<IActionResult> Edit(EditProductViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _service.UpdateAsync(model.ToDto());
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult UploadExcel()
    {
        return View(new ExcelUploadResultViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> UploadExcel(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ModelState.AddModelError("", "Please upload a valid Excel file.");
            return View(new ExcelUploadResultViewModel());
        }

        var rows = _excelReader.ReadProducts(file);
        var result = await _service.UploadFromExcelAsync(rows);

        return View(result.ToViewModel());
    }
}
