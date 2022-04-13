using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Purview.Cli.Commands.Scanning
{
    internal class DataSourcesRootCommand : Command
    {
        internal DataSourcesRootCommand() : base("datasource", "Work with Data Sources.")
        {
            this.AddCommand(new ListDataSourcesCommand("Get a list of all data sources from the purview account."));
            this.AddCommand(new DeleteDataSourceCommand("Delete a data source from a purview account."));
        }
    }
}
