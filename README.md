Run a powershell in the solution folder:

Set environment variable:
$env:SqlConnectionString="Server=tcp:localhost,1433;Initial Catalog=mybillsdb;User ID=sa;Password=P@ssw0rd;"

create initial script:
dotnet ef migrations add InitialCreate --project src\Infrastructure --startup-project src\Api --output-dir Persistence\Migrations

update database:
dotnet ef database update --project src\Infrastructure --startup-project src\Api

