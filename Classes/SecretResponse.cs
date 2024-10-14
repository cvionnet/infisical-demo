using Infisical.Sdk;

namespace infisical_demo;

internal class SecretResponse
{
    public string Environment { get; set; }
    public AuthenticationSecret Auth { get; set; }
    public InfisicalClient Client { get; set; }
}