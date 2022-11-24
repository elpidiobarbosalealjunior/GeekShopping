namespace GeekShopping.ProductApi.Data.ValueObjects;

public class CategoryVO
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    public ICollection<ProductVO>? Products { get; set; }
}
