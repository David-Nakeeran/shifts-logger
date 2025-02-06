using ShiftsLoggerAPI.Data;
using ShiftsLoggerAPI.Interface;

namespace ShiftsLoggerAPI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDbContext _context;

    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;
    }


}