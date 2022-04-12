// See https://aka.ms/new-console-template for more information
using Azure.Purview.Cli;
using System.CommandLine;
using Spectre.Console;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Diagnostics;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

//Debugger.Launch();

LoggingManager.SetupLogging();
var logger = LoggingManager.LoggerFactoryInstance.CreateLogger<Program>();

VersionInfo.PrintVerionInfo();


Console.WriteLine();

try
{
    var rootCommand = new CommandLineParser();
    await rootCommand.Command.InvokeAsync(args);
}
catch (Exception ex)
{
    logger.LogCritical($"A top level exception was caught.");
    logger.LogCritical($"Message: {ex.Message}");
}

await Task.Delay(10);
Console.WriteLine();
AnsiConsole.MarkupLine("Please let me know how i'm doing: [cyan1]https://github.com/martijnvanschie/azure-utilities-eventgrid[/]");