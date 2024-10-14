using Infisical.Sdk;

namespace infisical_demo.Services;

public class Secrets : ISecrets
{
    private readonly string _environment;
    private readonly InfisicalClient _client;

    public Secrets(string environment, InfisicalClient client)
    {
        _environment = environment;
        _client = client;
    }

    public GetSecretResponseSecret GetSecret(string projectId, string secretName, string path = "/")
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(secretName);

        var secretOptions = new GetSecretOptions
        {
            ProjectId = projectId,
            Environment = _environment,
            SecretName = secretName,
            Path = path,
        };

        return _client.GetSecret(secretOptions);
    }
}

public interface ISecrets
{
    GetSecretResponseSecret GetSecret(string projectId, string secretName, string path = "/");
}