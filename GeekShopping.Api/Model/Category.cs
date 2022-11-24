using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.ProductApi.Model;

[Table("category")]
public class Category
{
    [Key]
    [Required]
    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("name")]
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }
    public ICollection<Product>? Products { get; set; }
}
