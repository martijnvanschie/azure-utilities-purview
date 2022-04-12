# Azure Purview Utilities

![GitHub tag (latest by date)](https://img.shields.io/github/v/tag/martijnvanschie/azure-utilities-purview?label=Latest%20Release&logo=github) ![GitHub Release Date](https://img.shields.io/github/release-date/martijnvanschie/azure-utilities-purview?logo=github) ![GitHub Workflow Status](https://img.shields.io/github/workflow/status/martijnvanschie/azure-utilities-purview/Continues%20Integration?label=CI%20build&logo=github)

## Purpose

This is a small cli tool that let's you your Azure Purview using Command-Line interface.

## Getting started

### Download

To get started, download the [latest](https://github.com/martijnvanschie/azure-utilities-purview/releases/latest) release from the release page. Alternatively you can choose any of the pre-release versions available or just fork the code and build it yourself.

Currently the following downloads are available

| Artifact                                        | Description      	|
|-------------------------------------------------|------------------	|
| apv-win-x64-{version}.zip                	  | A windows x64 based version which requires .NET Runtime to be installed 	|

## Prerequisites

Depending on the artifact you download you need to have [.NET 6.0 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0/runtime) installed on your machine.

The Azure Purview cli expects a Purview account to be deployed for it to work. The information of the account is then used as options for the cli to manage objects in the account.

## Usage

Run the following command from the command line to get the cli usage information.

`apv -h`

### Delete a data source from a purview account

Example command that deletes a data source from your purview account

`apv datasource delete --account-name "my-purview-account" --datasource-name "my-datasource-name"`

### Diagnostics

The cli supports a diagnostics command, `apv diagnostics`, that can help in debugging authentication problems.

Run the following command to test the authentication and print out the token when successful.

`apv diagnostics token`

Run the following command to print out simple token claims to check basic information

`apv diagnostics credentials`

This will print out a table with the following claim information.

```bash
            Token Claims
+-----------------+-----------------+
| Key             | Value           |
|-----------------+-----------------|
| idtyp           | app             |
| app_displayname | some-name       |
| unique_name     | XXXXXX          |
| upn             | XXXXXX          |
+-----------------+-----------------+
```

### Authentication

Under the hood the tool uses a `TokenCredential` created by the `DefaultAzureCredential`. This means that you have some control over the way the cli will authenticate against your account.

[DefaultAzureCredential Class](https://docs.microsoft.com/en-us/dotnet/api/azure.identity.defaultazurecredential)

[Azure Identity client library for .NET - On Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/api/overview/azure/identity-readme)

[Azure Identity client library for .NET - On GitHub Repo](https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/identity/Azure.Identity/README.md)

## Enjoy

Just enjoy this really simple cli and use it well :)
