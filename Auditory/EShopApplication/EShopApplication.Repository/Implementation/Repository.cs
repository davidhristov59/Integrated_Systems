using System.Linq.Expressions;
using EShopApplication.Domain.DomainModels;
using EShopApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ArgumentNullException = System.ArgumentNullException;

namespace EShopApplication.Repository.Implementation;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _dbContext;
    private DbSet<T> entities; //referencira kon konkretna tabela na bazata na podatoci - dali Products, dali SH..

    public Repository(ApplicationDbContext dbContext, DbSet<T> entities)
    {
        _dbContext = dbContext;
        this.entities = entities;
    }

    public T Insert(T entity)
    {
        _dbContext.Add(entity);
        _dbContext.SaveChanges();
        
        return entity;
    }

    public T Update(T entity)
    {
        _dbContext.Update(entity);
        _dbContext.SaveChanges();

        return entity;
    }

    public T Delete(T entity)
    {
        _dbContext.Remove(entity);
        _dbContext.SaveChanges();

        return entity;
    }

    public E? Get<E>(Expression<Func<T, E>> selector, 
        Expression<Func<T, bool>>? predicate = null, 
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {

        IQueryable<T> query = entities;
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (include != null)
        {
            query = include(query);
        }

        if (orderBy != null)
        {
            return orderBy(query).Select(selector).FirstOrDefault();
        }

        return query.Select(selector).FirstOrDefault();
    }

    public IEnumerable<E> GetAll<E>(Expression<Func<T, E>> selector, 
        Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, 
            IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, 
            IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> query = entities;
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (include != null)
        {
            query = include(query);
        }

        if (orderBy != null)
        {
            return orderBy(query).Select(selector).AsEnumerable();
        }

        return query.Select(selector).AsEnumerable();
    }
}