namespace GeekShopping.OrderAPI.Messages;

public class ProductVO
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? ImageURL { get; set; }
    public int CategoryId { get; set; }
    public virtual CategoryVO? Category { get; set; }
}

