Run a powershell in the solution folder:

Set environment variable:
$env:SqlConnectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=mybillsdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"

create initial script:
dotnet ef migrations add InitialCreate --project src\Infrastructure --startup-project src\Api --output-dir Persistence\Migrations

update database:
dotnet ef database update --project src\Infrastructure --startup-project src\Api

