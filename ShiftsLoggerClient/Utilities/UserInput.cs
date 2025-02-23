using Spectre.Console;

namespace ShiftsLoggerClient.Utilities;

class UserInput
{
    private readonly Validation _validation;

    public UserInput(Validation validation)
    {
        _validation = validation;
    }
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

    internal long GetId(string message)
    {
        long userInput = AnsiConsole.Ask<long>(message);
        return userInput;
    }

    internal string GetEmployeeName(string message)
    {
        string userInput = AnsiConsole.Ask<string>(message);
        userInput = _validation.ValidateString(message, userInput);
        return userInput;
    }

    internal void WaitForUserInput()
    {
        AnsiConsole.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }
}