using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Interface;

public interface IEmployeeRepository : IRepository<Employee>
{
    public Task<List<Employee>> GetAllWithShiftsAsync();

    public Task<Employee> GetByIdWithShiftsAsync(long id);

    public Task<Employee> UpdateWithShiftsAsync(long id, EmployeeDTO entity);

}