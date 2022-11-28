using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartRepository _cartRepository;
    public CartController(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(_cartRepository));
    }

    [HttpGet("find-cart/{userId}")]
    public async Task<ActionResult<CartVO>> FindById(string userId)
    {
        var cart = await _cartRepository.FindByUserId(userId);
        if (cart == null) return NotFound();
        return Ok(cart);
    }


    [HttpPost("add-cart")]
    public async Task<ActionResult<CartVO>> AddCart(CartVO cartVO)
    {
        var cart = await _cartRepository.SaveOrUpdate(cartVO);
        if (cart == null) return NotFound();
        return Ok(cart);
    }


    [HttpPut("update-cart")]
    public async Task<ActionResult<CartVO>> UpdateCart(CartVO cartVO)
    {
        var cart = await _cartRepository.SaveOrUpdate(cartVO);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpDelete("remove-cart/{id}")]
    public async Task<ActionResult<CartVO>> RemoveCart(int id)
    {
        var status = await _cartRepository.RemoveFromByDetailId(id);
        if (!status) return BadRequest();
        return Ok(status);
    }

    [HttpPost("apply-coupon")]
    public async Task<ActionResult<CartVO>> ApplyCoupon(CartHeaderVO header)
    {
        var status = await _cartRepository.ApplyCoupon(header.UserId, header.CouponCode);
        if (status == false) return NotFound();
        return Ok(status);
    }

    [HttpDelete("remove-coupon/{userId}")]
    public async Task<ActionResult<CartVO>> RemoveCoupon(string userId)
    {
        var status = await _cartRepository.RemoveCoupon(userId);
        if (status == null) return NotFound();
        return Ok(status);
    }
}
