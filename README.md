# Azure Purview Utilities

## Purpose

This is a small cli tool that let's you your Azure Purview using Command-Line interface.

## Prerequisites

The Azure Purview cli expects a Purview account to be deployed for it to work. The information of the account is then used as options for the cli to manage objects in the account.

## Usage

Run the following command from the command line to get the cli usage information.

`apv -h`

### Delete a data source from a purview account

Example command that deletes a data source from your purview account

`apv datasource delete --account-name "my-purview-account" --datasource-name "my-datasource-name"`

### Authentication

Under the hood the tool uses a `TokenCredential` created by the `DefaultAzureCredential`. This means that you have some control over the way the cli will authenticate against your account.

[DefaultAzureCredential Class](https://docs.microsoft.com/en-us/dotnet/api/azure.identity.defaultazurecredential)

[Azure Identity client library for .NET - On Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/api/overview/azure/identity-readme)

[Azure Identity client library for .NET - On GitHub Repo](https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/identity/Azure.Identity/README.md)

## Enjoy

Just enjoy this really simple cli and use it well :)
