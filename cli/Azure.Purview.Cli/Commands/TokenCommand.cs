using Azure.Core;
using Spectre.Console;
using System.CommandLine;
using System.IdentityModel.Tokens.Jwt;

namespace Azure.Purview.Cli.Commands
{
    internal partial class DiagnosticsCommand
    {
        internal class TokenCommand : Command
        {
            public TokenCommand(string? description = null) : base("token", description)
            {
                AddOptions();
            }

            private void AddOptions()
            {
                this.SetHandler(async () =>
                {
                    AnsiConsole.MarkupLine($"Verifying the credential used for the Purview Cli.");

                    var cred = Reduct.Azure.CredentialsManager.GetDefaultCredential();

                    string[] scopes = new string[] { "https://graph.microsoft.com/.default" };

                    JwtSecurityToken? jsonToken = await AnsiConsole.Status()
                        .StartAsync($"Getting the token using [{Color.Aqua}]DefaultAzureCrential[/].", async ctx => {

                            ctx.Spinner(Spinner.Known.Dots);
                            ctx.SpinnerStyle(Style.Parse("aqua"));

                            var token = await cred.GetTokenAsync(new TokenRequestContext(scopes));
                            AnsiConsole.MarkupLine($"Token received.");

                            var handler = new JwtSecurityTokenHandler();
                            jsonToken = handler.ReadToken(token.Token) as JwtSecurityToken;
                            return jsonToken;
                        });

                    if (jsonToken is null)
                    {
                        AnsiConsole.MarkupLine($"No JSON token was received.");
                        return;
                    }

                    var value = jsonToken.RawData.EscapeMarkup();
                    var value2 = jsonToken.ToString().EscapeMarkup();

                    var panel = new Panel(value);
                    panel.Border = BoxBorder.Ascii;
                    panel.Header = new PanelHeader("RAW Data", Justify.Center);
                    panel.BorderColor(Color.Gold1);

                    AnsiConsole.Write(panel);

                    var panel2 = new Panel(value2);
                    panel2.Border = BoxBorder.Ascii;
                    panel2.Header = new PanelHeader("JSON Data", Justify.Center);
                    panel2.BorderColor(Color.Gold1);

                    AnsiConsole.Write(panel2);
                });
            }
        }
    }
}
