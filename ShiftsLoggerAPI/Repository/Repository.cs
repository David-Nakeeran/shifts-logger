using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Interface;

namespace ShiftsLoggerAPI.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<T> UpdateAsync(long id, T entity)
    {
        var existingEntity = await _dbSet.FindAsync(id);
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

    }

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var entityToBeDeleted = await _dbSet.FindAsync(id);
            if (entityToBeDeleted != null)
            {
                _dbSet.Remove(entityToBeDeleted);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            throw new Exception("Error occurred when trying to delete", ex);
        }
    }
}