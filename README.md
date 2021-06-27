Server
Beside the logic required by the project I added the following 
Used:
•	Sql server
•	Entity framework, Code first.
•	I built the DB tables using 
o	dotnet-ef migration add …
o	dotnet-ef database update
•	CORS with specific origins , (reading it from appsettings.json)
Added:
•	Swagger support
•	Docker support
•	IIS deployment support
Testing Notes:
•	In appsettings.json - the connection string needs to be replaced with the correct one 
•	In appsettings.json – AllowedOrigins (used for CORS WithOrigin ) values need to be replaced with the correct ones

