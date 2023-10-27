using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebApi.Core;

public interface IRepository<T> where T : Entity
{
    IQueryable<T> GetAll();

    bool Existed(Expression<Func<T, bool>> predicate);

    T? GetSingle(Expression<Func<T, bool>> predicate);

    IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

    void Add(T entity);

    void Update(T entity);
    void Removes(Expression<Func<T, bool>> predicate);

    void Remove(T entity);

    void Commit();
}

public class Repository<T> : IRepository<T> where T : Entity, new()
{
    private readonly DbContext _context;

    protected Repository(DbContext context)
    {
        _context = context;
    }

    public virtual IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public bool Existed(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Any(predicate);
    }

    public T? GetSingle(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().FirstOrDefault(predicate);
    }

    public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Where(predicate);
    }

    public virtual void Add(T entity)
    {
        EntityEntry dbEntityEntry = _context.Entry(entity);
        _context.Set<T>().Add(entity);
        dbEntityEntry.State = EntityState.Added;
    }

    public virtual void Update(T entity)
    {
        EntityEntry dbEntityEntry = _context.Entry(entity);
        dbEntityEntry.State = EntityState.Modified;
    }

    public void Removes(Expression<Func<T, bool>> predicate)
    {
        var entities = _context.Set<T>().Where(predicate);
        entities.ForEachAsync(Remove);
    }

    public virtual void Remove(T entity)
    {
        EntityEntry dbEntityEntry = _context.Entry(entity);
        dbEntityEntry.State = EntityState.Deleted;
    }

    public virtual void Commit()
    {
        _context.SaveChanges();
    }
}