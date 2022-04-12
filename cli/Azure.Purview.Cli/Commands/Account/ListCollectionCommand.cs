using Microsoft.Extensions.Logging;
using Spectre.Console;
using System.CommandLine;
using Reduct.Azure.Services.Purview.Account;
using Reduct.Azure.Services.Purview.Model;

namespace Azure.Purview.Cli.Commands.Account
{
    internal partial class CollectionsRootCommand
    {
        internal class ListCollectionCommand : Command
        {
            ILogger<ListCollectionCommand> _commandLogger = LoggingManager.LoggerFactoryInstance.CreateLogger<ListCollectionCommand>();
            ILogger<CollectionsClient> _clientLogger = LoggingManager.LoggerFactoryInstance.CreateLogger<CollectionsClient>();

            public ListCollectionCommand(string? description = null) : base("list", description)
            {
                AddOptions();
            }

            private void AddOptions()
            {
                var accountName = new Option<string>("--account-name", "The name of the Purview account.");
                accountName.AddAlias("-an");
                accountName.IsRequired = true;
                AddOption(accountName);

                this.SetHandler(async (string accountName) =>
                {
                    AnsiConsole.MarkupLine($"Getting all collections from account [cyan1]{accountName}[/].");

                    CollectionsClient _client = new CollectionsClient(accountName, AppState.Credential, _clientLogger);

                    var list = await _client.GetAllCollectionsAsync();
                    _commandLogger.LogInformation($"Received [{list.Collections.Count}] collections.");
                    await PrintCollectionTree(list.Collections);

                }, accountName);
            }

            private async Task PrintCollectionTree(List<Collection> list)
            {
                var rootCollection = list.Where(c => c.ParentCollection is null).FirstOrDefault();

                if (rootCollection is null)
                {
                    AnsiConsole.MarkupLine("No root collection found");
                }

                var root = new Tree($"[cyan1]{rootCollection.FriendlyName}[/]")
                    .Style("cyan1");
                
                BuildTree(root, null, rootCollection.Name, list);
                AnsiConsole.Write(root);
            }

            private void BuildTree(Tree? tree, TreeNode? rootNode, string parentCollection, List<Collection> list)
            {
                var childs = list.Where(c => c.ParentCollection?.ReferenceName == parentCollection).ToList();

                if (childs.Any() == false)
                {
                    return;
                }

                foreach (var collection in childs)
                {
                    if (tree is not null)
                    {
                        var node = tree.AddNode(collection.FriendlyName);
                        BuildTree(null, node, collection.Name, list);
                    }

                    if (rootNode is not null)
                    {
                        var node = rootNode.AddNode(collection.FriendlyName);
                        BuildTree(null, node, collection.Name, list);
                    }
                }
            }
        }
    }
}
