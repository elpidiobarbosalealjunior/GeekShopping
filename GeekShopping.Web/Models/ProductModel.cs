using System.ComponentModel.DataAnnotations;

namespace GeekShopping.Web.Models;

public class ProductModel
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
    public CategoryModel? Category { get; set; }
}
