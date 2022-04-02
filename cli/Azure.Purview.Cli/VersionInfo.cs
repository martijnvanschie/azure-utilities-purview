using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Purview.Cli
{
    internal class VersionInfo
    {
        public static void PrintVerionInfo()
        {
            var version = typeof(Program).Assembly.GetName().Version;
            Console.WriteLine($"{AppContext.BaseDirectory}");

            var path = Path.Combine(AppContext.BaseDirectory, "apv.exe");
            var fv = FileVersionInfo.GetVersionInfo(path);

            AnsiConsole.MarkupLine("");
            AnsiConsole.MarkupLine($"Event Purview Cli - [lightgoldenrod2_1]{fv.FileMajorPart}.{fv.FileMinorPart}.{fv.FileBuildPart}[/]");
            AnsiConsole.MarkupLine($"Part of the [cyan1]Azure Utilities Collection[/]");
            AnsiConsole.MarkupLine($"[dim]Build info - {fv.ProductVersion}[/]");
        }
    }
}
