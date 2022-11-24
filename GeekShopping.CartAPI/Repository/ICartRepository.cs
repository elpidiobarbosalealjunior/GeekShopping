using GeekShopping.CartAPI.Data.ValueObjects;

namespace GeekShopping.CartAPI.Repository;

public interface ICartRepository
{
    Task<CartVO> FindByUserId(string userId);
    Task<CartVO> SaveOrUpdate(CartVO cartVO);
    Task<bool> RemoveFromByDetailId(int cartDetailId);
    Task<bool> ApplyCoupon(string userId, string couponCode);
    Task<bool> RemoveCoupon(string userId);
    Task<bool> ClearCart(string userId);
}
