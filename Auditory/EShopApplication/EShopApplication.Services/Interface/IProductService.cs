using EShopApplication.Domain.DomainModels;
using EShopApplication.Domain.DTO;

namespace EShopApplication.Services.Interface;

public interface IProductService
{
    List<Product> GetAllProducts();
    Product? GetById (Guid id);
    Product Update(Product product);
    Product DeleteById(Guid id);
    Product Add(Product product);
    void AddToShoppingCart(Guid productId, Guid userId);
}