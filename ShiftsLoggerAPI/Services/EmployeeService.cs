using ShiftsLoggerAPI.Data;
using ShiftsLoggerAPI.Interface;
using ShiftsLoggerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ShiftsLoggerAPI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDbContext _context;

    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<Employee>>> GetAllEmployeesAsync()
    {

    }
    public async Task<ServiceResponse<Employee>> GetEmployeeByIdAsync(long id)
    {

    }
    public async Task<ServiceResponse<Employee>> CreateEmployee(EmployeeDTO employeeDTO)
    {

    }
    public async Task<ServiceResponse<Employee>> UpdateEmployee(long id, EmployeeDTO employeeDTO)
    {

    }
    public async Task<ServiceResponse<Employee>> DeleteEmployee(long id)
    {

    }

}