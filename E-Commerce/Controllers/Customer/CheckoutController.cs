using E_Commerce.Application.Interfaces.Services;
using E_Commerce.Models.Customer.Checkout;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers.Customer;


[Area("Customer")]
public class CheckoutController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ICartService _cartService;

    public CheckoutController(IOrderService orderService, ICartService cartService)
    {
        _orderService = orderService;
        _cartService = cartService;
    }

  

    public async Task<IActionResult> Index()
    {
        var cart = await _cartService.GetCartAsync();
        
        if (!cart.Items.Any())
        {
            TempData["Error"] = "Your cart is empty";
            return RedirectToAction("Index", "Products");
        }

        var vm = cart.ToCheckoutViewModel();
        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> PlaceOrder()
    {
        try
        {
            var cart = await _cartService.GetCartAsync();
            
            if (!cart.Items.Any())
            {
                TempData["Error"] = "Your cart is empty";
                return RedirectToAction("Index", "Products");
            }

            // Checkout process with transaction
            var order = await _orderService.CheckoutAsync(cart.Items);
            
            // Clear cart after successful order
            await _cartService.ClearCartAsync();
            
            return RedirectToAction("Confirmation", new { orderId = order.Id });
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    

    public async Task<IActionResult> Confirmation(int orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        
        if (order == null)
        {
            return NotFound();
        }
        

        var vm = order.ToConfirmationViewModel();
        return View(vm);
    }
}
