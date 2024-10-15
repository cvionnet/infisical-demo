A simple .NET 8 console app demonstrating how to read values from an on-premise Infisical server.

- Use the Universal Authentication method
- To create local secret, open a Terminal from the project folder:
> - dotnet user-secrets init
> - dotnet user-secrets set "Infisical:ClientId" "infisical-client-id"
> - dotnet user-secrets set "Infisical:ClientSecret" "infisical-client-token"
> - dotnet user-secrets set "Infisical:ClientProjectId" "your-project-id"