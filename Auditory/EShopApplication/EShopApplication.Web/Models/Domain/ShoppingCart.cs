using EShopApplication.Web.Models.Identity;

namespace EShopApplication.Web.Models;

public class ShoppingCart
{
    public Guid Id { get; set; }
    public string OwnerId { get; set; } 
    public EShopApplicationUser user  { get; set; }
    public virtual ICollection<ProductInShoppingCart> ProductsInShoppingCarts { get; set; }
}