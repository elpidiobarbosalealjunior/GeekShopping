using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices;

public interface ICouponService
{
    Task<CouponViewModel> GetByCouponCode(string couponCode, string token);
}
