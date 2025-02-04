using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Data;
using ShiftsLoggerAPI.Interface;
using ShiftsLoggerAPI.Models;


namespace ShiftsLoggerAPI.Services;

public class ShiftService : IShiftService
{
    private readonly ApplicationDbContext _context;

    public ShiftService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Shift>> GetAllShiftsAsync()
    {
        return await _context.Shifts
            .Include(s => s.Employee)
            .ToListAsync();
    }

    public async Task<Shift> GetShiftByIdAsync(long id)
    {
        var shift = await _context.Shifts
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.ShiftId == id);

        return shift;
    }

    public async Task<Shift> UpdateShift(long id, ShiftDTO shiftDTO)
    {
        var savedShift = await _context.Shifts
            .Include(s => s.Employee)
            .FirstOrDefaultAsync(x => x.ShiftId == id);

        if (savedShift == null)
        {
            return null;
        }
        savedShift.StartTime = shiftDTO.StartTime;
        savedShift.EndTime = shiftDTO.EndTime;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!ShiftExists(id))
        {
            return null;
        }

        return savedShift;
    }

    private bool ShiftExists(long id)
    {
        return _context.Shifts.Any(e => e.ShiftId == id);
    }

}