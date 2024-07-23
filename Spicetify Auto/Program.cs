using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Spectre.Console;

class Program
{
    static void Main()
    {
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Hearts)
            .Start("[red]Checking OS You have...[/]", ctx =>
            {
                // Simulate some processing by sleeping.
                System.Threading.Thread.Sleep(2000);

            });
        var rule = new Rule("[rapidblink red]By GravityG[/]");
        rule.Style = Style.Parse("orange3");
        rule.Justification = Justify.Center;

; AnsiConsole.Write(
    new FigletText("Spicetify Auto installer.")
        .Centered()
        .Color(Color.Red));

        AnsiConsole.Write(rule);
        AnsiConsole.MarkupLine("Disclaimer: [dodgerblue3]I'm not the creator of Spicetify just contributing to help make things easy.[/]");
        string OS = RuntimeInformation.OSDescription;
        AnsiConsole.MarkupLine($"System: [dodgerblue3]{OS}[/]");
        AnsiConsole.MarkupLine("Press Enter to continue or W to open the Spicetify website...");
        var key = Console.ReadKey();

        if (key.KeyChar == 'w' || key.KeyChar == 'W')
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://spicetify.app/",
                UseShellExecute = true
            });
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        var psi = new ProcessStartInfo();
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;
        psi.UseShellExecute = false;

        if (OS.Contains("Windows"))
        {
            psi.FileName = "powershell";
            psi.Arguments = "-Command \"& {iwr -useb https://raw.githubusercontent.com/spicetify/cli/main/install.ps1 | iex}\"";
        }
        else // for Unix-based systems like macOS and Linux
        {
            psi.FileName = "/bin/bash";
            psi.Arguments = "-c \"$(curl -fsSL https://raw.githubusercontent.com/spicetify/cli/main/install.sh) | sh\"";
        }

        var process = Process.Start(psi);

        process.WaitForExit();
        AnsiConsole.MarkupLine(process.StandardOutput.ReadToEnd());
        AnsiConsole.MarkupLine(process.StandardError.ReadToEnd());
    }
}
