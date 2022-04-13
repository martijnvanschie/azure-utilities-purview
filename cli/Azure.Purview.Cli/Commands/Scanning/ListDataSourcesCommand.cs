using Spectre.Console;
using System.CommandLine;
using Reduct.Azure.Services.Purview.Scanning;
using Microsoft.Extensions.Logging;
using Reduct.Azure.Services.Purview.Model;

namespace Azure.Purview.Cli.Commands.Scanning
{
    internal class ListDataSourcesCommand : Command
    {
        ILogger<ListDataSourcesCommand> _commandLogger = LoggingManager.LoggerFactoryInstance.CreateLogger<ListDataSourcesCommand>();
        ILogger<DataSourceClient> _dsClientLogger = LoggingManager.LoggerFactoryInstance.CreateLogger<DataSourceClient>();

        public ListDataSourcesCommand(string? description = null) : base("list", description)
        {
            AddOptions();
        }

        private void AddOptions()
        {
            var accountName = new Option<string>("--account-name", "The name of the Purview account.");
            accountName.AddAlias("-an");
            accountName.IsRequired = true;
            AddOption(accountName);

            var filter = new Option<string>("--filter", "A filter to apply to the data source names to list.");
            filter.AddAlias("-f");
            filter.SetDefaultValue("");
            filter.IsRequired = false;
            AddOption(filter);

            this.SetHandler(async (string accountName, string filter) =>
            {
                AnsiConsole.MarkupLine($"Listing all data sources [cyan1]{accountName}[/].");

                DataSourceClient _client = new DataSourceClient(accountName, AppState.Credential, _dsClientLogger);
                var list = await _client.ListAllDataSourceAsync();
                PrintTree(list.DataSources, filter);
            }, accountName, filter);
        }

        private void PrintTree(List<DataSource> list, string filter)
        {
            var groupedList = list.GroupBy(ds => ds.Properties?.Collection?.ReferenceName).ToList();

            var tree = new Tree("...")
                .Style("cyan1");

            foreach (var group in groupedList)
            {
                var collectionNode = new TreeNode(new Markup(group.Key));

                foreach (var groupedItem in group.Where(g => g.Name.Contains(filter)))
                    collectionNode.AddNode($"[cyan1]{groupedItem.Name}[/] - [grey70]{groupedItem.Kind}[/]");

                if (collectionNode.Nodes.Count > 0)
                    tree.Nodes.Add(collectionNode);
            }

            AnsiConsole.Write(tree);
        }

    }
}
