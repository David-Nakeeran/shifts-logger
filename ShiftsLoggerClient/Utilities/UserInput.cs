using Spectre.Console;

namespace ShiftsLoggerClient.Utilities;

public class UserInput
{
    public string ShowMenu()
    {
        string[] menuOptions = {
            "View all employees",
            "Create employee",
            "Update employee",
            "Delete employee",
            "View all shifts",
            "Create shift",
            "Update shift",
            "Delete shift",
            "Quit application"
            };

        // Console.Clear();
        AnsiConsole.MarkupLine("[bold]Main Menu[/]");
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[green](Use arrow keys to navigate, then press enter)[/]");
        AnsiConsole.WriteLine();
        var userSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option")
                .AddChoices(menuOptions)
        );
        return userSelection;
    }

    internal int GetId()
    {
        int shiftId = AnsiConsole.Ask<int>("Please enter the id of shift");
        return shiftId;
    }

    internal void WaitForUserInput()
    {
        AnsiConsole.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }
}