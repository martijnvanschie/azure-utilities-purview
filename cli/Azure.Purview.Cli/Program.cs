﻿// See https://aka.ms/new-console-template for more information
using Azure.Purview.Cli;
using System.CommandLine;
using Spectre.Console;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

//Debugger.Launch();
LoggingManager.SetupLogging();
var logger = LoggingManager.LoggerFactoryInstance.CreateLogger<Program>();

VersionInfo.PrintVerionInfo();
Console.WriteLine();

var rootCommand = new CommandLineParser();

try
{
    await rootCommand.Command.InvokeAsync(args);
}
catch (Exception ex)
{
    logger.LogCritical($"A top level exception was caught.");
    logger.LogCritical($"{ex.Message}");
}

Console.WriteLine();
AnsiConsole.MarkupLine("Please let me know how i'm doing: [cyan1]https://github.com/martijnvanschie/azure-utilities-eventgrid[/]");