using EShopApplication.Domain.DomainModels;
using EShopApplication.Domain.DTO;
using EShopApplication.Repository.Interface;
using EShopApplication.Services.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Exception = System.Exception;

namespace EShopApplication.Services.Implementation;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
    private readonly IShoppingCartService _shoppingCartService;

    public ProductService(IRepository<Product> productRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository, IShoppingCartService shoppingCartService)
    {
        _productRepository = productRepository;
        _productInShoppingCartRepository = productInShoppingCartRepository;
        _shoppingCartService = shoppingCartService;
    }
    
    public List<Product> GetAllProducts()
    {
        return _productRepository
            .GetAll(selector: x => x) //returns the product entity
            .ToList();
    }
    
    public Product? GetById(Guid id)
    {
        return _productRepository.Get(
            selector: x => x,
            predicate: x => x.Id == id);
    }
    
    public Product Update(Product product)
    {
        return _productRepository.Update(product);
    }
    
    public Product DeleteById(Guid id)
    {
        var product = GetById(id);
    
        if (product == null)
        {
            throw new Exception("This product does not exist");
        }
    
        _productRepository.Delete(product);
        return product;
    }
    
    public Product Add(Product product)
    {
        product.Id = Guid.NewGuid();
        return _productRepository.Insert(product);
    }
    
    public void AddToShoppingCart(Guid productId, Guid userId)
    {
        var shoppingCart = _shoppingCartService.GetByUserId(userId);
    
        var product = _productRepository
            .Get(selector: x => x,
                predicate: x => x.Id == productId);
    
        var existingProductsInShoppingCart = _productInShoppingCartRepository
            .Get(selector: x => x,
                predicate: x => x.ProductID == productId && x.ShoppingCartID == shoppingCart.Id);
    
        if (existingProductsInShoppingCart != null)
        {
            existingProductsInShoppingCart.Quantity++;
            _productInShoppingCartRepository.Update(existingProductsInShoppingCart);
        }
        else
        {
            ProductInShoppingCart productInShoppingCart = new ProductInShoppingCart()
            {
                Id = Guid.NewGuid(),
                Product = product,
                ProductID = productId,
                Quantity = 1,
                ShoppingCart = shoppingCart,
                ShoppingCartID = shoppingCart.Id
            };
            
            _productInShoppingCartRepository.Insert(productInShoppingCart);
        }
    }
}