using GeekShopping.CouponAPI.Data.ValueObjects;
using GeekShopping.CouponAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CouponAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CouponController : ControllerBase
{
    private readonly ICouponRepository _couponRepository;
    public CouponController(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(_couponRepository));
    }

    [HttpGet("{couponCode}")]
    public async Task<ActionResult<CouponVO>> GetByCouponCode(string couponCode)
    {
        var coupon = await _couponRepository.GetByCouponCode(couponCode);
        if (coupon == null) return NotFound();
        return Ok(coupon);
    }
}
