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

            private bool _incudeNames = false;

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

                var includeName = new Option<bool>("--include-name", "Specifies if the result should include the collection name in addition to the friendly name.");
                includeName.AddAlias("-n");
                includeName.SetDefaultValue(false);
                includeName.IsRequired = false;
                AddOption(includeName);

                this.SetHandler(async (string accountName, bool includeName) =>
                {
                    AnsiConsole.MarkupLine($"Getting all collections from account [cyan1]{accountName}[/].");

                    CollectionsClient _client = new CollectionsClient(accountName, AppState.Credential, _clientLogger);

                    var list = await _client.GetAllCollectionsAsync();
                    _commandLogger.LogInformation($"Received [{list.Collections.Count}] collections.");
                    await PrintCollectionTree(list.Collections, includeName);

                }, accountName, includeName);
            }

            private async Task PrintCollectionTree(List<Collection> list, bool includeName)
            {
                _incudeNames = includeName;

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
                    var nodeName = collection.FriendlyName;
                    if (_incudeNames)
                    {
                        nodeName += $" [grey70][[{collection.Name}]][/]";
                    }


                    if (tree is not null)
                    {
                        var node = tree.AddNode(nodeName);
                        BuildTree(null, node, collection.Name, list);
                    }

                    if (rootNode is not null)
                    {
                        var node = rootNode.AddNode(nodeName);
                        BuildTree(null, node, collection.Name, list);
                    }
                }
            }
        }
    }
}
