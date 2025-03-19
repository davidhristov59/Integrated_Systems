namespace EShopApplication.Web.Models.DTO;

public class AddProductToShoppingCartDTO
{
    public Guid ProductID { get; set; }
    public Product selectedProduct { get; set; }
    public int Quantity { get; set; }
}