using Infisical.Sdk;
using infisical_demo;
using infisical_demo.Helpers;
using Microsoft.Extensions.Configuration;

var environment = Environment.GetEnvironmentVariable("CORE_ENVIRONMENT") ?? "development";

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .Build();

var authentication = new AuthenticationSecret
{
    ServerUrl = builder["Infisical:Url"],
    ClientId = builder["Infisical:ClientId"],
    ClientSecret = builder["Infisical:ClientSecret"],
    ProjectId = builder["Infisical:ProjectId"],
};

ClientSettings clientSettings = AuthenticationHelper.InitializeClientSettings(authentication);
SecretResponse secretResponse = new SecretResponse
{
    Environment = environment,
    Auth = authentication,
    Client = new InfisicalClient(clientSettings)
};

try
{
    var secret1 = SecretsHelper.GetSecret(secretResponse, "ANOTHER_LOGIN");
    Console.WriteLine($"Value of {secret1.SecretKey} is: \n{secret1.SecretValue}\n\n");

    var secret2 = SecretsHelper.GetSecret(secretResponse, "CONNECTION_STRING", "/client1");
    Console.WriteLine($"Value of {secret2.SecretKey} is: \n{secret2.SecretValue}\n\n");
}
catch (InfisicalException ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}