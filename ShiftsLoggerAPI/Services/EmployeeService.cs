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
        ServiceResponse<List<Employee>> _response = new ServiceResponse<List<Employee>>();

        try
        {
            var employeeList = await _context.Employees
                .Include(x => x.Shifts)
                .ToListAsync();

            _response.Success = true;
            _response.Message = "Ok";
            _response.Data = employeeList;

        }
        catch (Exception ex)
        {
            _response.Success = false;
            _response.Message = "Error";
            _response.Data = null;
        }
        return _response;

    }
    public async Task<ServiceResponse<Employee>> GetEmployeeByIdAsync(long id)
    {
        ServiceResponse<Employee> _response = new ServiceResponse<Employee>();

        try
        {
            var employee = await _context.Employees
                .Include(x => x.Shifts)
                .FirstOrDefaultAsync(employee => employee.EmployeeId == id);

            _response.Success = true;
            _response.Message = "Ok";
            _response.Data = employee;

        }
        catch (Exception ex)
        {
            _response.Success = false;
            _response.Message = "Error";
            _response.Data = null;
        }
        return _response;
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