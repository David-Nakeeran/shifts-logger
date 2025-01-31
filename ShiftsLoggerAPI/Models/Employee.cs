namespace ShiftsLoggerAPI.Models;

public class Employee
{
    public long EmployeeId { get; set; }
    public string Name { get; set; }
    public List<Shift> Shifts { get; set; }
}