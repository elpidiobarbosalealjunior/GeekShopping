using System.ComponentModel.DataAnnotations;

namespace GeekShopping.Web.Models;

public class ProductViewModel
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
    public CategoryViewModel? Category { get; set; }

    [Range(1, 100)]
    public int Count { get; set; } = 1;

    public string SubstringName()
    {
        if (this.Name?.Length < 24) return this.Name;
        return $"{this.Name?.Substring(0, 21)}...";
    }

    public string SubstringDescription()
    {
        if (this.Description?.Length < 355) return this.Description;
        return $"{this.Name?.Substring(0, 352)}...";
    }
}
