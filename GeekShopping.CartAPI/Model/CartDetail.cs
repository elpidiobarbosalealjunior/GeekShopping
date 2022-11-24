using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartAPI.Model;

[Table("cart_detail")]
public class CartDetail
{
    [Key]
    [Required]
    [Column("cart_detail_id")]
    public int CartDetailId { get; set; }

    [Column("cart_header_id")]
    public int CartHeaderId { get; set; }
    public virtual CartHeader CartHeader { get; set; }

    [Column("product_id")]
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }

    [Column("count")]
    public int Count { get; set; }
}
