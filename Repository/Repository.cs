
using Microsoft.EntityFrameworkCore;
using Westcoast.web.Data;
using Westcoast.web.Interface;
using Westcoast.web.Models;

namespace Westcoast.web.Repository;

public class Repository<T> : IRepository<T> where T : class
{
        protected readonly WestcoastContext _context;
        protected readonly DbSet<T> _entity;
    public Repository(WestcoastContext context)
    {
            _context = context;
            _entity = _context.Set<T>();
    }

    public async Task<bool> AddAsync(T entity)
    {
        await _entity.AddAsync(entity);
        return true;
    }

    public Task<bool> DeleteAsync(T entity)
    {
        try
            {
                _entity.Remove(entity);
            return Task.FromResult(true);
            }
            catch 
            {
                
                return Task.FromResult(false);
            }
    }

    public async Task<T?> FindByIdAsync(int id)
    {
        return await _entity.FindAsync(id);
    }

    public async Task<IList<T>> ListAllAsync()
    {
        return await _entity.ToListAsync();
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<bool> UpdateAsync(T entity)
    {
        try
            {
                _entity.Update(entity);
                return Task.FromResult(true);
            }
            catch
            {
                
                return Task.FromResult(false);
            }
    }
}
