using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.OrderAPI.Model;

[Table("order_detail")]
public class OrderDetail
{
    [Key]
    [Required]
    [Column("order_detail_id")]
    public int OrderDetailId { get; set; }

    [ForeignKey("order_header_id")]
    public int OrderHeaderId { get; set; }
    public virtual OrderHeader OrderHeader { get; set; }

    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("count")]
    public int Count { get; set; }

    [Column("product_name")]
    public string? ProductName { get; set; }

    [Column("price")]
    public decimal Price { get; set; }
}
