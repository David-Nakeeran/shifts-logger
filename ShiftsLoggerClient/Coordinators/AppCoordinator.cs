

using ShiftsLoggerClient.Display;
using ShiftsLoggerClient.Services;
using ShiftsLoggerClient.Utilities;

namespace ShiftsLoggerClient.Coordinators;

class AppCoordinator
{
    private readonly UserInput _userInput;
    private readonly ShiftService _shiftService;
    private readonly DisplayManager _displayManager;

    public AppCoordinator(UserInput userInput, ShiftService shiftService, DisplayManager displayManager)
    {
        _userInput = userInput;
        _shiftService = shiftService;
        _displayManager = displayManager;
    }
    internal async Task Start()
    {
        bool isAppActive = true;

        while (isAppActive)
        {
            var userSelection = _userInput.ShowMenu();

            switch (userSelection)
            {
                case "View all employees":
                    Console.WriteLine("all employees");
                    break;
                case "Create employee":
                    Console.WriteLine("create employee");
                    break;
                case "Update employee":
                    Console.WriteLine("update employee");
                    break;
                case "Delete employee":
                    Console.WriteLine("delete employee");
                    break;
                case "View all shifts":
                    Console.WriteLine("all shifts");
                    await AllShifts();
                    break;
                case "Create shift":
                    Console.WriteLine("create shift");
                    break;
                case "Update shift":
                    Console.WriteLine("update shift");
                    break;
                case "Delete shift":
                    Console.WriteLine("delete shift");
                    await GetShiftById();
                    break;
                case "Quit application":
                    isAppActive = false;
                    break;
            }

        }
    }

    public async Task AllShifts()
    {
        var shifts = await _shiftService.GetAllShifts();
        _displayManager.RenderGetAllShiftsTable(shifts);
        _userInput.WaitForUserInput();
    }

    internal async Task GetShiftById()
    {
        var shifts = await _shiftService.GetAllShifts();
        _displayManager.RenderGetAllShiftsTable(shifts);
        _userInput.WaitForUserInput();
        var displayId = _userInput.GetId();
        int count = 1;
        long shiftId;
        foreach (var shift in shifts)
        {
            if (count == displayId)
            {
                shiftId = shift.ShiftId;
                var shiftById = await _shiftService.GetShiftById(shiftId);
                _displayManager.RenderGetShiftByIdTable(shiftById);
                _userInput.WaitForUserInput();
            }
            count++;

        }

    }
}