using Infisical.Sdk;
using Microsoft.Extensions.Configuration;

var environment = Environment.GetEnvironmentVariable("CORE_ENVIRONMENT");

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

// ---------------------------

ClientSettings clientSettings = Authentication(authentication);
SecretResponse secretResponse = new SecretResponse
{
    Environment = environment,
    Auth = authentication,
    Client = new InfisicalClient(clientSettings)
};

try
{
    var secret1 = GetSecret(secretResponse, "ANOTHER_LOGIN");
    Console.WriteLine($"Value of {secret1.SecretKey} is: \n{secret1.SecretValue}\n\n");

    var secret2 = GetSecret(secretResponse, "CONNECTION_STRING", "/client1");
    Console.WriteLine($"Value of {secret2.SecretKey} is: \n{secret2.SecretValue}\n\n");
}
catch (InfisicalException ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

#region METHODS

static ClientSettings Authentication(AuthenticationSecret auth)
{
    var clientSettings = new ClientSettings
    {
        SiteUrl = auth.ServerUrl,
        Auth = new AuthenticationOptions
        {
            UniversalAuth = new UniversalAuthMethod
            {
                ClientId = auth.ClientId,
                ClientSecret = auth.ClientSecret
            }
        }
    };

    return clientSettings;
}

static GetSecretResponseSecret GetSecret(SecretResponse secretResponse, string secretName, string path = "/")
{
    var secretOptions = new GetSecretOptions
    {
        ProjectId = secretResponse.Auth.ProjectId,
        Environment = secretResponse.Environment,
        SecretName = secretName,
        Path = path,
    };

    return secretResponse.Client.GetSecret(secretOptions);
}

#endregion METHODS

#region CLASSES

internal class AuthenticationSecret
{
    public string ServerUrl { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string ProjectId { get; set; }
}

internal class SecretResponse
{
    public string Environment { get; set; }
    public AuthenticationSecret Auth { get; set; }
    public InfisicalClient Client { get; set; }
}

#endregion CLASSES