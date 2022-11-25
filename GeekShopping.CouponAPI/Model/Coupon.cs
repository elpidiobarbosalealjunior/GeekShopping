using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CouponAPI.Model
{
    [Table("coupon")]
    public class Coupon
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("coupon_id")]
        public int CouponId { get; set; }

        [Column("coupon_code")]
        [Required]
        [StringLength(30)]
        public string? CouponCode { get; set; }

        [Column("discount_amount")]
        [Required]
        public decimal DiscountAmount { get; set; }
    }
}
