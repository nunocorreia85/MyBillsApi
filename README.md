![Build Status](https://github.com/nunocorreia85/MyBillsApi/workflows/BuildTest/badge.svg?branch=master) [![Coverage Status](https://coveralls.io/repos/github/nunocorreia85/MyBillsApi/badge.svg?branch=master)](https://coveralls.io/github/nunocorreia85/MyBillsApi?branch=master) [![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/mikuam/TicketStore/blob/master/LICENSE)

# MyBills API
This is a project showing how to build a Azure Functions API in .Net Core 3.1


Guide manual:
In powershell run the following:

Set environment variable:

- $env:SqlConnectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=mybillsdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"

update database:
- dotnet ef database update -p src\Infrastructure -s src\Api

In case any domein entity is changed please run this command:

add migration InitialCreate:
- dotnet ef migrations add InitialCreate -p src\Infrastructure -s src\Api -o Persistence\Migrations


## Architecture

![diagram concept](.github/ArchitectureDiagram.png)
