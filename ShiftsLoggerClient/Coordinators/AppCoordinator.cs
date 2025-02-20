

using ShiftsLoggerClient.Display;
using ShiftsLoggerClient.Models;
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

    public async Task<List<ShiftDTO>> GetAllShifts()
    {
        var shifts = await _shiftService.GetAllShifts();
        _displayManager.RenderGetAllShiftsTable(shifts);
        return shifts;
    }

    internal async Task AllShifts()
    {
        await GetAllShifts();
        _userInput.WaitForUserInput();
    }

    internal async Task<Dictionary<long, long>> GetKeyValuePairs()
    {
        var shifts = await GetAllShifts();
        var keyValuePairs = new Dictionary<long, long>();
        long displayId = 1;

        foreach (var shift in shifts)
        {
            keyValuePairs[displayId] = shift.ShiftId;
            displayId++;
        }
        return keyValuePairs;
    }

    internal async Task<ShiftDTO> GetShiftById()
    {
        var shifts = await GetAllShifts();
        _userInput.WaitForUserInput();

        var displayId = _userInput.GetId();
        var pairs = await GetKeyValuePairs();
        long shiftId;

        foreach (var element in pairs)
        {
            if (element.Key == displayId)
            {
                shiftId = element.Value;
                return await _shiftService.GetShiftById(shiftId);
            }
        }
        return new ShiftDTO { };
        // _displayManager.RenderGetShiftByIdTable(shiftById);
        // _userInput.WaitForUserInput();


        // int count = 1;
        // long shiftId;
        // foreach (var shift in shifts)
        // {
        //     if (count == displayId)
        //     {
        //         shiftId = shift.ShiftId;
        //         var shiftById = await _shiftService.GetShiftById(shiftId);
        //         _displayManager.RenderGetShiftByIdTable(shiftById);
        //         _userInput.WaitForUserInput();
        //     }
        //     count++;

        // }
    }

    internal async Task DeleteShift()
    {
        var result = await _shiftService.DeleteShiftById(shiftId);

        if (result.Success)
        {
            // display manager method to show message on success
        }
        else
        {
            // display manager method to show error status code and message
        }
    }
}