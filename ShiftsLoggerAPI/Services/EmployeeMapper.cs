using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Services;

public class EmployeeMapper
{
    public EmployeeDTO employeeToDTO(Employee employee) =>
        new EmployeeDTO
        {
            EmployeeId = employee.EmployeeId,
            Name = employee.Name,
            ShiftId = employee.Shifts.Select(x => x.ShiftId).ToList()
        };
}