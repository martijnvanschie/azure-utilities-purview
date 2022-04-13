using Azure.Purview.Cli.Commands;
using Azure.Purview.Cli.Commands.Account;
using Azure.Purview.Cli.Commands.Scanning;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Purview.Cli
{
    internal class CommandLineParser
    {
        internal RootCommand Command { get; private set; }

        public CommandLineParser()
        {
            Command = new RootCommand();
            Command.Description = "Azure Purview Cli";

            Command.AddCommand(new DataSourcesRootCommand());
            Command.AddCommand(new CollectionsRootCommand());
            Command.AddCommand(new DiagnosticsCommand());
        }
    }
}
