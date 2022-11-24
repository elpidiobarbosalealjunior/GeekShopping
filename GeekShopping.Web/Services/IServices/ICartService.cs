using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices;

public interface ICartService
{
    Task<CartViewModel> FindByUserId(string userId, string token);
    Task<CartViewModel> AddItem(CartViewModel cart, string token);
    Task<CartViewModel> Update(CartViewModel cart, string token);
    Task<bool> RemoveFrom(int cartId, string token);
    Task<bool> ApplayCoupon(CartViewModel cart, string couponCode, string token);
    Task<bool> RemoveCoupon(string userId, string token);
    Task<bool> Clear(string userId, string token);
    Task<CartViewModel> Checkout(CartHeaderViewModel cartHeader, string token);
}
