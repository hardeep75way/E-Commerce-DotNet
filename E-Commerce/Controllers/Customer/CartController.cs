using E_Commerce.Application.Interfaces.Services;
using E_Commerce.Models.Customer.Cart;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers.Customer;


[Area("Customer")]
public class CartController : Controller
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
        var cartDto = await _cartService.GetCartAsync();
        var vm = cartDto.ToViewModel();
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
    {
        try
        {
            await _cartService.AddToCartAsync(productId, quantity);
            return RedirectToAction("Index");
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Index", "Products");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateQuantity(int productId, int quantity)
    {
        try
        {
            await _cartService.UpdateQuantityAsync(productId, quantity);
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int productId)
    {
        await _cartService.RemoveFromCartAsync(productId);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Clear()
    {
        await _cartService.ClearCartAsync();
        return RedirectToAction("Index");
    }
}
