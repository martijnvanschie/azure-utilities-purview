using Spectre.Console;
using System.CommandLine;
using Reduct.Azure.Services.Purview.Scanning;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Azure.Purview.Cli.Commands.Scanning
{
    internal class DeleteDataSourceCommand : Command
    {
        ILogger<DeleteDataSourceCommand> _commandLogger = LoggingManager.LoggerFactoryInstance.CreateLogger<DeleteDataSourceCommand>();
        ILogger<DataSourceClient> _dsClientLogger = LoggingManager.LoggerFactoryInstance.CreateLogger<DataSourceClient>();

        public DeleteDataSourceCommand(string? description = null) : base("delete", description)
        {
            AddOptions();
        }

        private void AddOptions()
        {
            var accountName = new Option<string>("--account-name", "The name of the Purview account.");
            accountName.AddAlias("-an");
            accountName.IsRequired = true;
            AddOption(accountName);

            var dataSourceName = new Option<string>("--datasource-name", "The name of the data source.");
            dataSourceName.AddAlias("-ds");
            dataSourceName.IsRequired = true;
            AddOption(dataSourceName);

            this.SetHandler(async (string accountName, string dataSourceName) =>
            {
                AnsiConsole.MarkupLine($"Deleteing data source [cyan1]{dataSourceName}[/] from account [cyan1]{accountName}[/].");

                DataSourceClient _client = new DataSourceClient(accountName, AppState.Credential, _dsClientLogger);
                await _client.DeleteDataSourceAsync(dataSourceName);

            }, accountName, dataSourceName);
        }
    }
}
