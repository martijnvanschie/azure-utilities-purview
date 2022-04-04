using Azure.Core;
using Spectre.Console;
using System.CommandLine;
using System.IdentityModel.Tokens.Jwt;

namespace Azure.Purview.Cli.Commands
{
    internal partial class DiagnosticsCommand
    {
        internal class CredentialCommand : Command
        {
            public CredentialCommand(string? description = null) : base("credentials", description)
            {
                AddAlias("cred");
                AddOptions();
            }

            private void AddOptions()
            {
                this.SetHandler(async () =>
                {
                    AnsiConsole.MarkupLine($"Verifying the credential used for the Purview Cli.");

                    var cred = Reduct.Azure.CredentialsManager.GetDefaultCredential();

                    string[] scopes = new string[] { "https://graph.microsoft.com/.default" };

                    AnsiConsole.MarkupLine($"Trying to get the token using [{Color.Aqua}]DefaultAzureCrential[/].");
                    var token = await cred.GetTokenAsync(new TokenRequestContext(scopes));
                    AnsiConsole.MarkupLine($"Token received.");

                    AnsiConsole.MarkupLine("");

                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(token.Token) as JwtSecurityToken;

                    var idtyp = jsonToken?.Claims.First(c => c.Type == "idtyp").Value;
                    var app_displayname = jsonToken?.Claims.First(c => c.Type == "app_displayname").Value;
                    var unique_name = jsonToken?.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
                    var upn = jsonToken?.Claims.FirstOrDefault(c => c.Type == "upn")?.Value;

                    //AnsiConsole.MarkupLine($"Azure token idtyp: [{Color.Cyan1}][[{idtyp}]][/].");
                    //AnsiConsole.MarkupLine($"Azure token app_displayname: [{Color.Cyan1}][[{app_displayname}]][/].");
                    //AnsiConsole.MarkupLine($"Azure token unique_name: [{Color.Cyan1}][[{unique_name}]][/].");
                    //AnsiConsole.MarkupLine($"Azure token upn: [{Color.Cyan1}][[{upn}]][/].");

                    // Create a table
                    var table = new Table();
                    table.Border = TableBorder.Ascii2;
                    table.Title = new TableTitle($"[{Color.Aqua}]Token Claims[/]");

                    // Add some columns
                    table.AddColumn($"[{Color.LightGoldenrod2_1}]Key[/]");
                    table.AddColumn($"[{Color.LightGoldenrod2_1}]Value[/]");

                    // Add some rows
                    table.AddRow("idtyp", $"[{Color.Cyan1}]{idtyp}[/]");
                    table.AddRow("app_displayname", $"[{Color.Cyan1}]{app_displayname}[/]");
                    table.AddRow("unique_name", $"[{Color.Cyan1}]{unique_name}[/]");
                    table.AddRow("upn", $"[{Color.Cyan1}]{upn}[/]");

                    // Render the table to the console
                    AnsiConsole.Write(table);
                });
            }
        }
    }
}
