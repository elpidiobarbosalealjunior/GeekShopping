using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers;

public class CartController : Controller
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly ICouponService _couponService;

    public CartController(IProductService productService, ICartService cartService, ICouponService couponService)
    {
        _productService = productService;
        _cartService = cartService;
        _couponService = couponService;
    }

    [Authorize]
    public async Task<IActionResult> CartIndex()
    {            
        return View(await FindUserCart());
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> CheckoutIndex()
    {
        return View(await FindUserCart());
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CheckoutIndex(CartViewModel model)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var response = await _cartService.Checkout(model.CartHeader, token);
        if(response != null)
        {
            return RedirectToAction(nameof(ConfirmationIndex));
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmationIndex()
    {
        return View();
    }

    [HttpPost]
    [ActionName("ApplyCoupon")]
    public async Task<IActionResult> ApplyCoupon(CartViewModel model)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
        var response = await _cartService.ApplyCoupon(model.CartHeader, token ?? "");
        if (response)
        {
            return RedirectToAction(nameof(CartIndex));
        }
        return View();
    }

    [HttpPost]
    [ActionName("RemoveCoupon")]
    public async Task<IActionResult> RemoveCoupon()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
        var response = await _cartService.RemoveCoupon(userId, token ?? "");
        if (response)
        {
            return RedirectToAction(nameof(CartIndex));
        }
        return View();
    }

    public async Task<IActionResult> Remove(int id)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
        var response = await _cartService.RemoveFrom(id, token ?? "");
        if (response)
        {
            return RedirectToAction(nameof(CartIndex));
        }
        return View();
    }

    private async Task<CartViewModel> FindUserCart()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

        var response = await _cartService.FindByUserId(userId ?? "", token ?? "");
        if (response?.CartHeader != null)
        {
            if (!string.IsNullOrEmpty(response.CartHeader.CouponCode))
            {
                var coupon = await _couponService.GetByCouponCode(response.CartHeader.CouponCode, token);
                if(coupon?.CouponCode != null)
                {
                    response.CartHeader.DiscountAmount = coupon.DiscountAmount;
                }
            }
            foreach (var detail in response.CartDetails)
            {
                response.CartHeader.PurchaseAmount += (detail.Product.Price * detail.Count);
            }
            response.CartHeader.PurchaseAmount -= response.CartHeader.DiscountAmount;
        }

        return response ?? new CartViewModel();
    }
}
