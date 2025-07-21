namespace Momon.Biju.Web.Areas.Cart.Models;

public class CartViewModel
{
    public List<CartItem> Items { get; set; }

    public decimal SumPriceByProduct(Guid productId)
    {
        return Items.Where(x => x.ProductId == productId).Sum(x => x.Price);
    }
}