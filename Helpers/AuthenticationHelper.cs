using Infisical.Sdk;

namespace infisical_demo.Helpers;

internal static class AuthenticationHelper
{
    internal static ClientSettings InitializeClientSettings(AuthenticationSecret auth)
    {
        ArgumentNullException.ThrowIfNull(auth);

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