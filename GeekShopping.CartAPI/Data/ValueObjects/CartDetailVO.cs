namespace GeekShopping.CartAPI.Data.ValueObjects;

public class CartDetailVO
{
    public int CartDetailId { get; set; }
    public int CartHeaderId { get; set; }
    public virtual CartHeaderVO CartHeader { get; set; }
    public int ProductId { get; set; }
    public virtual ProductVO Product { get; set; }
    public int Count { get; set; }
}
