using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Purview.Cli.Commands.Account
{
    internal partial class CollectionsRootCommand : Command
    {
        internal CollectionsRootCommand() : base("collection", "Work with Data Sources.")
        {
            this.AddCommand(new ListCollectionCommand("List all collections from a purview account."));
            this.AddCommand(new DeleteCollectionCommand("Delete a data source from a purview account."));
        }
    }
}
