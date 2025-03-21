using EShopApplication.Domain.DomainModels;
using EShopApplication.Domain.DTO;

namespace EShopApplication.Services.Interface;

public interface IProductService
{
    List<Product> GetAllProducts();
    Product GetDetailsForProducts (Guid? id);
    void CreateNewProduct(Product product);
    void UpdateExistingProduct(Product product);
    AddProductToShoppingCartDTO GetShoppingCartInfo(Guid? productId);
    void DeleteProduct(Product product);
    bool AddToShoppingCart(AddProductToShoppingCartDTO item, string userID);

}