using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Interface;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Repository;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(DbContext context) : base(context)
    {
    }
    public Task<List<Employee>> GetAllWithShiftsAsync()
    {

    }

    public Task<Employee> GetByIdWithShiftsAsync(long id)
    {

    }

    public Task<Employee> UpdateWithShiftsAsync(long id, EmployeeDTO entity)
    {

    }

    public async Task DeleteAsync(long id)
    {
        var entityToBeDeleted = await _dbSet.FindAsync(id);
        if (entityToBeDeleted != null)
        {
            _dbSet.Remove(entityToBeDeleted);
            await _context.SaveChangesAsync();
        }

    }

    public Task<List<Employee>> GetAllWithShiftsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Employee> GetByIdWithShiftsAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Employee> UpdateWithShiftsAsync(long id, EmployeeDTO entity)
    {
        throw new NotImplementedException();
    }
}