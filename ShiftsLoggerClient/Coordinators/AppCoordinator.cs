

using ShiftsLoggerClient.Utilities;

namespace ShiftsLoggerClient.Coordinators;

class AppCoordinator
{
    private readonly UserInput _userInput;

    public AppCoordinator(UserInput userInput)
    {
        _userInput = userInput;
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
}