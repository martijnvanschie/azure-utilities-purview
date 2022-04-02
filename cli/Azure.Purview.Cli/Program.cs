// See https://aka.ms/new-console-template for more information
using Azure.Purview.Cli;
using System.CommandLine;
using Spectre.Console;
using System.Diagnostics;

Debugger.Launch();
VersionInfo.PrintVerionInfo();
Console.WriteLine();

var rootCommand = new CommandLineParser();
await rootCommand.Command.InvokeAsync(args);

Console.WriteLine();
AnsiConsole.MarkupLine("Please let me know how i'm doing: [cyan1]https://github.com/martijnvanschie/azure-utilities-eventgrid[/]");