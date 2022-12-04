using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GeekShopping.OrderAPI.Model;

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

