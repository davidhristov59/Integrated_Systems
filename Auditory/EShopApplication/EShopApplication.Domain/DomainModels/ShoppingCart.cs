
using EShopApplication.Domain.IdentityModels;

namespace EShopApplication.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity 
    {
        public string OwnerId { get; set; }
        public EShopApplicationUser user { get; set; }
        public virtual ICollection<ProductInShoppingCart> ProductsInShoppingCarts { get; set; }
    }
}