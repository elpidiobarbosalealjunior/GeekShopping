using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.Api.Model;

[Table("product")]
public class Product
{
    [Key]
    [Required]
    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("name")]
    [Required]
    [StringLength(150)]
    public string? Name { get; set; }

    [Column("price")]
    [Required]
    [Range(1, 10000)]
    public decimal Price { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("stock")]
    [Required]
    [Range(0,1000)]
    public int Stock { get; set; }

    [Column("image_url")]
    [StringLength(300)]
    public string? ImageURL { get; set; }

    [Column("category_id")]
    [Required]
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
}
