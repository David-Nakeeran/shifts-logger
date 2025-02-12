using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Interface;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Repository;

public class ShiftRepository : Repository<Shift>, IShiftRepository
{
    public ShiftRepository(DbContext context) : base(context)
    {
    }
    public async Task<List<Shift>> GetAllWithEmployeeAsync()
    {
        try
        {
            return await _dbSet
                        .Include(s => s.Employee)
                        .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return new List<Shift>();
    }

    public async Task<Shift> GetByIdWithEmployeeAsync(long id)
    {
        try
        {
            var shift = await _dbSet
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.ShiftId == id);

            if (shift != null)
            {
                return shift;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return null;
    }

    public Task<Shift> UpdateWithEmployeeAsync(long id, ShiftDTO entity)
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

}