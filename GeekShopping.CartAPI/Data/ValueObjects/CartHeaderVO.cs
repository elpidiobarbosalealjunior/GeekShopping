namespace GeekShopping.CartAPI.Data.ValueObjects;

public class CartHeaderVO
{
    public int CartHeaderId { get; set; }
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }
}
