using Microsoft.Extensions.Logging;
using Spectre.Console;
using System.CommandLine;
using Reduct.Azure.Services.Purview.Account;

namespace Azure.Purview.Cli.Commands.Account
{
    internal partial class CollectionsRootCommand
    {
        internal class DeleteCollectionCommand : Command
        {
            ILogger<DeleteCollectionCommand> _commandLogger = LoggingManager.LoggerFactoryInstance.CreateLogger<DeleteCollectionCommand>();
            ILogger<CollectionsClient> _clientLogger = LoggingManager.LoggerFactoryInstance.CreateLogger<CollectionsClient>();

            public DeleteCollectionCommand(string? description = null) : base("delete", description)
            {
                AddOptions();
            }

            private void AddOptions()
            {
                var accountName = new Option<string>("--account-name", "The name of the Purview account.");
                accountName.AddAlias("-an");
                accountName.IsRequired = true;
                AddOption(accountName);

                var collectionName = new Option<string>("--collection-name", "The name of the data source.");
                collectionName.AddAlias("-c");
                collectionName.IsRequired = true;
                AddOption(collectionName);

                this.SetHandler(async (string accountName, string collectionName) =>
                {
                    AnsiConsole.MarkupLine($"Trying to delete collection [cyan1]{collectionName}[/] from account [cyan1]{accountName}[/].");

                    CollectionsClient _client = new CollectionsClient(accountName, AppState.Credential, _clientLogger);
                    await _client.DeleteCollectionAsync(collectionName);

                }, accountName, collectionName);
            }
        }
    }
}
