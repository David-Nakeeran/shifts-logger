using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Interface;

public interface IShiftService
{
    public Task<List<Shift>> GetAllShiftsAsync();
    public Task<Shift> GetShiftByIdAsync(long id);
    public Task<Shift> CreateShift(Shift shift);
    public Task<Shift> UpdateShift(long id, ShiftDTO shiftDTO);
    public string? DeleteShift(long id);
}