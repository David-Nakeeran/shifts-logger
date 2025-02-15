namespace ShiftsLoggerAPI.Models;

public class ShiftDTO
{
    public long ShiftId { get; set; }
    public long EmployeeId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

}