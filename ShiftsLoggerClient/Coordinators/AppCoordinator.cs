

using ShiftsLoggerClient.Services;
using ShiftsLoggerClient.Utilities;

namespace ShiftsLoggerClient.Coordinators;

class AppCoordinator
{
    private readonly UserInput _userInput;
    private readonly ShiftService _shiftService;

    public AppCoordinator(UserInput userInput, ShiftService shiftService)
    {
        _userInput = userInput;
        _shiftService = shiftService;
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
        foreach (var item in shifts)
        {
            Console.WriteLine(item.ShiftId.ToString());
            Console.WriteLine(item.EmployeeId.ToString());
            Console.WriteLine(item.StartTime.ToString());
            Console.WriteLine(item.EndTime.ToString());
            Console.WriteLine(item.Name.ToString());
            Console.WriteLine("------------------");
        }
    }
}