using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Services;

public class ShiftMapper
{
    public ShiftDTO shiftToDTO(Shift shift) =>
        new ShiftDTO
        {
            ShiftId = shift.ShiftId,
            EmployeeId = shift.EmployeeId,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            Name = shift.Employee.Name
        };
}