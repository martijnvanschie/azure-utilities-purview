using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Purview.Cli.Commands
{
    internal partial class DiagnosticsCommand : Command
    {
        internal DiagnosticsCommand() : base("diagnostics", "Perform some diagnostics on the cli environment.")
        {
            this.AddCommand(new CredentialCommand("Display information about the used Azure Credential."));
            this.AddCommand(new TokenCommand("Display the JWT token information from the current Azure Credential."));
        }
    }
}
