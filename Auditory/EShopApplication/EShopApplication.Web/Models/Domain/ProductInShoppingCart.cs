namespace EShopApplication.Web.Models;

public class ProductInShoppingCart
{
    public Guid ProductID { get; set; }
    public Product Product { get; set; }
    public Guid ShoppingCartID { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    public int Quantity { get; set; }
}