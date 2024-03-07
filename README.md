1. Create Postgres Db (e.g. with Azure Database for Postgres)
2. Update connection string in `appsettings.json`
3. Execute `dotnet ef database update`
4. Insert test data into db with `data.sql`
5. Run the app
6. Access the endpoint at `http://localhost:19505`