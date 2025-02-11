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

            if (employee == null)
            {
                _response.Success = false;
                _response.Message = "NotFound";
                _response.Data = null;
                return _response;
            }

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
        ServiceResponse<Employee> _response = new ServiceResponse<Employee>();

        try
        {
            var employee = new Employee
            {
                Name = employeeDTO.Name,
            };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var employeeWithShifts = await _context.Employees
                .Include(x => x.Shifts)
                .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

            if (employeeWithShifts == null)
            {
                _response.Success = false;
                _response.Message = "NotFound";
                _response.Data = null;
                return _response;
            }

            _response.Success = true;
            _response.Message = "Ok";
            _response.Data = employeeWithShifts;
        }
        catch (Exception ex)
        {
            _response.Success = false;
            _response.Message = "Error";
            _response.Data = null;
        }
        return _response;
    }
    public async Task<ServiceResponse<Employee>> UpdateEmployee(long id, EmployeeDTO employeeDTO)
    {
        ServiceResponse<Employee> _response = new ServiceResponse<Employee>();
        try
        {
            var savedEmployee = await _context.Employees
                        .Include(x => x.Shifts)
                        .FirstOrDefaultAsync(x => x.EmployeeId == id);

            if (savedEmployee == null)
            {
                _response.Success = false;
                _response.Message = "NotFound";
                _response.Data = null;
                return _response;
            }

            savedEmployee.Name = employeeDTO.Name;
            await _context.SaveChangesAsync();

            _response.Success = true;
            _response.Message = "Updated";
            _response.Data = savedEmployee;
        }
        catch (Exception ex)
        {
            _response.Success = false;
            _response.Message = "Error";
            _response.Data = null;
        }
        return _response;
    }
    public async Task<ServiceResponse<Employee>> DeleteEmployee(long id)
    {
        ServiceResponse<Employee> _response = new ServiceResponse<Employee>();
        try
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                _response.Success = false;
                _response.Message = "NotFound";
                _response.Data = null;
                return _response;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            _response.Success = false;
            _response.Message = "Error";
            _response.Data = null;
        }
        return _response;
    }

}