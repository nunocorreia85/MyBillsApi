![Build Status](https://github.com/nunocorreia85/MyBillsApi/workflows/BuildDeploy/badge.svg?branch=github-actions) [![Coverage Status](https://coveralls.io/repos/github/nunocorreia85/MyBillsApi/badge.svg?branch=master)](https://coveralls.io/github/nunocorreia85/MyBillsApi?branch=master)

# MyBills API
This is a project showing how to build a Azure Functions API in .Net Core 3.1


NOTES:
Run a powershell in the solution folder:

Set environment variable:

- $env:SqlConnectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=mybillsdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"

create initial script:
- dotnet ef migrations add InitialCreate --project src\Infrastructure --startup-project src\Api --output-dir Persistence\Migrations

update database:
- dotnet ef database update --project src\Infrastructure --startup-project src\Api

