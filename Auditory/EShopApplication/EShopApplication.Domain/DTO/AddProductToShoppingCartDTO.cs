using EShopApplication.Domain.DomainModels;

namespace EShopApplication.Domain.DTO
{
    public class AddProductToShoppingCartDTO
    {
        public Guid ProductID { get; set; }
        public Product selectedProduct { get; set; }
        public int Quantity { get; set; }
    }
}