using ShiftsLoggerClient.Models;
using ShiftsLoggerClient.Utilities;
using Spectre.Console;

namespace ShiftsLoggerClient.Display;

class DisplayManager
{
    private readonly CalculateDuration _calculateDuration;

    public DisplayManager(CalculateDuration calculateDuration)
    {
        _calculateDuration = calculateDuration;
    }
    internal void RenderGetAllShiftsTable(List<ShiftDTO> shifts)
    {
        var table = new Table();
        table.AddColumns("DisplayId", "Employee Name", "Shift Start", "Shift End", "Duration");
        int count = 1;
        foreach (var shift in shifts)
        {
            var duration = _calculateDuration.CalcDuration(shift.StartTime, shift.EndTime);
            var start = shift.StartTime.ToString("HH:mm");
            var end = shift.StartTime.ToString("HH:mm");
            table.AddRow($"{count}", $"{shift.Name}", $"{start}", $"{end}", $"{duration.ToString(@"hh\:mm")}");
            count++;
        }
        AnsiConsole.Write(table);
    }

    internal void RenderGetShiftByIdTable(ShiftDTO shift)
    {
        var table = new Table();
        table.AddColumns("Employee Name", "Shift Start", "Shift End", "Duration");


        var duration = _calculateDuration.CalcDuration(shift.StartTime, shift.EndTime);
        var start = shift.StartTime.ToString("HH:mm");
        var end = shift.StartTime.ToString("HH:mm");
        table.AddRow($"{shift.Name}", $"{start}", $"{end}", $"{duration.ToString(@"hh\:mm")}");


        AnsiConsole.Write(table);
    }
}