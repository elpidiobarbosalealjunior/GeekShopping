namespace GeekShopping.CouponAPI.Data.ValueObjects
{
    public class CouponVO
    {
        public int CouponId { get; set; }
        public string? CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
