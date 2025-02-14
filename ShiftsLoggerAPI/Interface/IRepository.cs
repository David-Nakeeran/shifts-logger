

namespace ShiftsLoggerAPI.Interface;

public interface IRepository<T> where T : class
{
    public Task<List<T>> GetAllAsync();

    public Task<T> GetByIdAsync(long id);

    public Task AddAsync(T entity);

    public Task<T> UpdateAsync(long id, T entity);

    public Task<bool> DeleteAsync(long id);

}