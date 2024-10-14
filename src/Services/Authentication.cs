using Infisical.Sdk;

namespace infisical_demo.Services;

public class Authentication : IAuthentication
{
    public ClientSettings InitializeClientSettings(AuthenticationSecret auth)
    {
        ArgumentNullException.ThrowIfNull(auth);
        ArgumentException.ThrowIfNullOrWhiteSpace(auth.ServerUrl);
        ArgumentException.ThrowIfNullOrWhiteSpace(auth.ClientId);
        ArgumentException.ThrowIfNullOrWhiteSpace(auth.ClientSecret);

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
}

public interface IAuthentication
{
    ClientSettings InitializeClientSettings(AuthenticationSecret auth);
}