using EShopApplication.Domain.DomainModels;
using EShopApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using ArgumentNullException = System.ArgumentNullException;

namespace EShopApplication.Repository.Implementation;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _dbContext;
    private DbSet<T> entities; //referencira kon konkretna tabela na bazata na podatoci - dali Products, dali SH..
    string errorMessage = string.Empty;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        entities = dbContext.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        return entities.AsEnumerable();
    }

    public T Get(Guid? id)
    {
        return entities.SingleOrDefault(p => p.Id == id);
    }

    public void Insert(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }
        entities.Add(entity);
        _dbContext.SaveChanges();
    }

    public void Update(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }
        entities.Update(entity);
        _dbContext.SaveChanges();
    }

    public void Delete(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }
        entities.Remove(entity);
    }
}