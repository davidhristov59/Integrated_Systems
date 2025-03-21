using EShopApplication.Domain.DomainModels;

namespace EShopApplication.Domain.DomainModels
{
    public class ProductInShoppingCart : BaseEntity
    {
        public Guid ProductID { get; set; }
        public Product Product { get; set; }
        public Guid ShoppingCartID { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}