using ShiftsLoggerAPI.Validators;

namespace ShiftsLoggerAPI.Models;

public class ShiftDTO
{
    public long ShiftId { get; set; }

    public long EmployeeId { get; set; }

    public DateTime StartTime { get; set; }


    [EndTimeGreaterThanStartTime(ErrorMessage = "EndTime must be greater than StartTime")]
    public DateTime EndTime { get; set; }

    public string EmployeeName { get; set; }

}