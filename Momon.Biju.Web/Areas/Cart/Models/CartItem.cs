namespace Momon.Biju.Web.Areas.Cart.Models;

public class CartItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal  Price { get; set; }
}