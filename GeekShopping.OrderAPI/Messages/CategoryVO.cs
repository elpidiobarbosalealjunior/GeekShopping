namespace GeekShopping.OrderAPI.Messages;
public class CategoryVO
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    public ICollection<ProductVO>? Products { get; set; }
}

