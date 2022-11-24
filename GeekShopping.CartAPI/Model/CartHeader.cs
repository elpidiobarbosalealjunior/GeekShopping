using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartAPI.Model;

[Table("cart_header")]
public class CartHeader
{
    [Key]
    [Required]
    [Column("cart_header_id")]
    public int CartHeaderId { get; set; }

    [Column("user_id")]
    public string? UserId { get; set; }

    [Column("coupon_code")]
    public string? CouponCode { get; set; }
}
