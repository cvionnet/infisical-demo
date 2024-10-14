using Infisical.Sdk;
using infisical_demo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace infisical_demo;

internal class Program
{
    private static void Main(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("CORE_ENVIRONMENT") ?? "development";

        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets<Program>()
            .Build();

        var serviceProvider = ConfigureServices(builder, environment);

        var authenticationService = serviceProvider.GetService<IAuthentication>();
        var secretsService = serviceProvider.GetService<ISecrets>();

        AuthenticationSecret authenticationSecret = SetupAuthenticationSecret(builder);

        try
        {
            var secret1 = secretsService?.GetSecret(authenticationSecret.ProjectId, "ANOTHER_LOGIN");
            Console.WriteLine($"Value of {secret1?.SecretKey} is: \n{secret1?.SecretValue}\n\n");

            var secret2 = secretsService?.GetSecret(authenticationSecret.ProjectId, "CONNECTION_STRING", "/client1");
            Console.WriteLine($"Value of {secret2?.SecretKey} is: \n{secret2?.SecretValue}\n\n");
        }
        catch (InfisicalException ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static ServiceProvider ConfigureServices(IConfigurationRoot builder, string environment)
    {
        var services = new ServiceCollection();
        services.AddSingleton<IAuthentication, Authentication>();
        services.AddSingleton<ISecrets>(provider =>
        {
            var authenticationService = provider.GetService<IAuthentication>();

            AuthenticationSecret authentication = SetupAuthenticationSecret(builder);
            ClientSettings clientSettings = authenticationService.InitializeClientSettings(authentication);
            var client = new InfisicalClient(clientSettings);

            return new Secrets(environment, client);
        });

        return services.BuildServiceProvider();
    }

    private static AuthenticationSecret SetupAuthenticationSecret(IConfigurationRoot builder)
    {
        return new AuthenticationSecret
        {
            ServerUrl = builder["Infisical:Url"] ?? string.Empty,
            ClientId = builder["Infisical:ClientId"] ?? string.Empty,
            ClientSecret = builder["Infisical:ClientSecret"] ?? string.Empty,
            ProjectId = builder["Infisical:ProjectId"] ?? string.Empty,
        };
    }
}