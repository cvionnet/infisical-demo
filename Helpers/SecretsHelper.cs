using Infisical.Sdk;

namespace infisical_demo.Helpers;

internal static class SecretsHelper
{
    internal static GetSecretResponseSecret GetSecret(SecretResponse secretResponse, string secretName, string path = "/")
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
}