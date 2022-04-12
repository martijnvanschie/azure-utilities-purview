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
        internal Argument<bool> _debug = new Argument<bool>("--debug", "Attaches the debugger to the cli process.");


        internal RootCommand Command { get; private set; }

        public CommandLineParser()
        {
            Command = new RootCommand();
            Command.Description = "Azure Purview Cli";

            AddDefaultOptions();

            Command.AddCommand(new DataSourcesRootCommand());
            Command.AddCommand(new CollectionsRootCommand());
            Command.AddCommand(new DiagnosticsCommand());
        }

        internal void AddDefaultOptions()
        {
            _debug.SetDefaultValue(false);
            //Command.AddArgument(_debug);


            //var accountName = new Option<bool>("--debug", "Attaches the debugger to the cli process.");
            //accountName.IsRequired = false;
            //Command.AddOption(accountName);
        }
    }
}
