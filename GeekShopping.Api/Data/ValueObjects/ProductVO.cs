namespace GeekShopping.ProductApi.Data.ValueObjects;

public class ProductVO
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public int Stock { get; set; }
    public string? ImageURL { get; set; }
    public int CategoryId { get; set; }
    public CategoryVO? Category { get; set; }
}
