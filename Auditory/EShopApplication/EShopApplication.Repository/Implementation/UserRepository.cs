using EShopApplication.Domain.IdentityModels;
using EShopApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EShopApplication.Repository.Implementation;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    private DbSet<EShopApplicationUser> entities; 
    string errorMessage = string.Empty;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        entities = dbContext.Set<EShopApplicationUser>();
    }

    public IEnumerable<EShopApplicationUser> GetAll()
    {
        return entities.AsEnumerable();
    }

    public EShopApplicationUser Get(string id)
    {
        return entities
            .Include(z => z.ShoppingCart)
            .Include("ShoppingCart.ProductsInShoppingCarts")
            .Include("ShoppingCart.ProductsInShoppingCarts.Product")
            .SingleOrDefault(z => z.Id == id);
    }

    public void Insert(EShopApplicationUser entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }

        entities.Add(entity);
        _dbContext.SaveChanges();
    }

    public void Update(EShopApplicationUser entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }
        entities.Update(entity);
        _dbContext.SaveChanges();
    }

    public void Delete(EShopApplicationUser entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }
        entities.Remove(entity);
    }
}