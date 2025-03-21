using EShopApplication.Domain.DomainModels;
using EShopApplication.Domain.DTO;
using EShopApplication.Repository.Interface;
using EShopApplication.Services.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EShopApplication.Services.Implementation;

public class ProductService : IProductService
{
    private readonly IRepository<Product> repository;
    private readonly IRepository<ProductInShoppingCart> productInShoppingCartRepository;
    private readonly IUserRepository _userRepository;

    public ProductService(IRepository<Product> repository, IUserRepository userRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository)
    {
        this.repository = repository;
        _userRepository = userRepository;
        this.productInShoppingCartRepository = productInShoppingCartRepository;
    }

    public List<Product> GetAllProducts()
    {
        var products = repository.GetAll();
        if (products == null)
        {
            return new List<Product>();
        }
        return products.ToList();
    }

    public Product GetDetailsForProducts(Guid? id)
    {
        return repository.Get(id);
    }

    public void CreateNewProduct(Product product)
    {
        //product.Id = Guid.NewGuid();
        this.repository.Insert(product);
    }

    public void UpdateExistingProduct(Product product)
    {
        repository.Update(product);
    }

    public AddProductToShoppingCartDTO GetShoppingCartInfo(Guid? productId)
    {
        var product = repository.Get(productId);

        AddProductToShoppingCartDTO productDto = new AddProductToShoppingCartDTO()
        {
            ProductID = product.Id,
            selectedProduct = product,
            Quantity = 1
        };

        return productDto;
    }

    public void DeleteProduct(Product product)
    {
        repository.Delete(product);
    }

    public bool AddToShoppingCart(AddProductToShoppingCartDTO item, string userID)
    {
        var user = _userRepository.Get(userID);

        var userShoppingCart = user.ShoppingCart;
        
        if (userShoppingCart != null && item.ProductID != null)
        {
            var product = GetDetailsForProducts(item.ProductID);

            if (product != null)
            {
                ProductInShoppingCart productInShoppingCart = new ProductInShoppingCart()
                {
                    Product = product,
                    ProductID = item.ProductID,
                    ShoppingCart = userShoppingCart,
                    ShoppingCartID = userShoppingCart.Id,
                    Quantity = item.Quantity
                };
                productInShoppingCartRepository.Insert(productInShoppingCart);
                return true;
            }

            return false;
        }
        return false;
    }
    
}