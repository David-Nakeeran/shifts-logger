

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
    private readonly EmployeeService _employeeService;

    public AppCoordinator(UserInput userInput, ShiftService shiftService, DisplayManager displayManager, EmployeeService employeeService)
    {
        _userInput = userInput;
        _shiftService = shiftService;
        _displayManager = displayManager;
        _employeeService = employeeService;
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
                    await AllEmployees();
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
                    await DeleteShift();
                    break;
                case "Quit application":
                    isAppActive = false;
                    break;
            }

        }
    }

    internal async Task<ApiResponse<List<EmployeeDTO>>> GetAllEmployees()
    {
        var employees = await _employeeService.GetAllEmployees();
        return employees;
    }

    internal async Task AllEmployees()
    {
        var employees = await GetAllEmployees();
        _displayManager.RenderGetAllEmployeesTable(employees.Data);
        _userInput.WaitForUserInput();
    }

    public async Task<List<ShiftDTO>> GetAllShifts()
    {
        var shifts = await _shiftService.GetAllShifts();
        return shifts;
    }

    internal async Task AllShifts()
    {
        var shifts = await GetAllShifts();
        _displayManager.RenderGetAllShiftsTable(shifts);
        _userInput.WaitForUserInput();
    }

    internal async Task<Dictionary<long, long>> GetKeyValuePairsShifts()
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

    internal async Task<Dictionary<long, long>> GetKeyValuePairsEmployees()
    {
        var employees = await GetAllEmployees();
        var keyValuePairs = new Dictionary<long, long>();
        long displayId = 1;

        foreach (var employee in employees.Data)
        {
            keyValuePairs[displayId] = employee.EmployeeId;
            displayId++;
        }
        return keyValuePairs;
    }

    internal async Task<ShiftDTO> GetShiftById()
    {
        var shifts = await GetAllShifts();
        _userInput.WaitForUserInput();

        var displayId = _userInput.GetId();
        var pairs = await GetKeyValuePairsShifts();
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
    }

    internal async Task DeleteShift()
    {
        await AllShifts();

        var displayId = _userInput.GetId("Please enter the id of shift");
        var pairs = await GetKeyValuePairsEmployees();

        if (!pairs.ContainsKey(displayId))
        {
            _displayManager.IncorrectId();
            _userInput.WaitForUserInput();
            return;
        }

        long shiftId = pairs[displayId];

        var result = await _shiftService.DeleteShiftById(shiftId);

        if (result.Success)
        {
            _displayManager.ShowMessage(result.Message);
            _userInput.WaitForUserInput();
        }
        else
        {
            _displayManager.ShowMessage(result.Message);
            _userInput.WaitForUserInput();
        }
    }

    internal async Task<ApiResponse<EmployeeDTO>> GetEmployeeById(long id)
    {

    }

    internal async Task PostShift()
    {
        // display all employees
        await AllEmployees();

        // get id of employee to add shift to
        var displayId = _userInput.GetId("Please enter the id of the employee you want create a shift for");

        var idPairs = await GetKeyValuePairsEmployees();

        if (!idPairs.ContainsKey(displayId))
        {
            _displayManager.IncorrectId();
            _userInput.WaitForUserInput();
            return;
        }

        long employeeId = idPairs[displayId];

        // get employee by id
        // get name and store in variable
        // get start and end time in variables
    }
}