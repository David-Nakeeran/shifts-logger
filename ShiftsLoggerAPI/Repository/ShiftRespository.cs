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
            throw new Exception("Error fetching all employees", ex);
        }
    }

    public async Task<Shift> GetByIdWithEmployeeAsync(long id)
    {
        try
        {
            var shift = await _dbSet
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.ShiftId == id);

            if (shift == null)
            {
                return null;
            }
            return shift;

        }
        catch (Exception ex)
        {
            throw new Exception("Error finding shift", ex);
        }
    }

    public async Task<Shift> UpdateWithEmployeeAsync(long id, ShiftDTO entity)
    {
        try
        {
            var savedShift = await _dbSet
                        .Include(s => s.Employee)
                        .FirstOrDefaultAsync(x => x.ShiftId == id);

            if (savedShift == null)
            {
                return null;
            }
            _dbSet.Entry(savedShift).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return savedShift;

        }
        catch (Exception ex)
        {
            throw new Exception("Error updating the shift", ex);
        }
    }

    public async Task DeleteAsync(long id)
    {
        await base.DeleteAsync(id);
    }

}