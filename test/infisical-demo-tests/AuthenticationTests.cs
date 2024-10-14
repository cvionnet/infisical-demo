using FluentAssertions;
using infisical_demo;
using infisical_demo.Services;

namespace infisical_demo_tests;

public class AuthenticationTests
{
    private readonly Authentication _sut;

    public AuthenticationTests()
    {
        _sut = new Authentication();
    }

    [Fact]
    public void InitializeClientSettings_ShouldReturnClientSettings_WhenParametersAreValid()
    {
        // Arrange
        var auth = new AuthenticationSecret
        {
            ServerUrl = "https://example.com",
            ClientId = "123456",
            ClientSecret = "secret"
        };

        // Act
        var result = _sut.InitializeClientSettings(auth);

        // Assert
        result.Should().NotBeNull();
        result.SiteUrl.Should().Be(auth.ServerUrl);
        result.Auth.Should().NotBeNull();
        result.Auth.UniversalAuth.Should().NotBeNull();
        result.Auth.UniversalAuth.ClientId.Should().Be(auth.ClientId);
        result.Auth.UniversalAuth.ClientSecret.Should().Be(auth.ClientSecret);
    }

    [Fact]
    public void InitializeClientSettings_ShouldThrowArgumentNullException_WhenAuthIsNull()
    {
        // Act
        var action = new Action(() => _sut.InitializeClientSettings(null));

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void InitializeClientSettings_ShouldThrowArgumentException_WhenServerUrlIsIncorrect(string serverUrl)
    {
        // Arrange
        var authenticationSecret = new AuthenticationSecret
        {
            ServerUrl = serverUrl,
            ClientId = "123456",
            ClientSecret = "secret"
        };

        // Act
        var action = new Action(() => _sut.InitializeClientSettings(authenticationSecret));

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void InitializeClientSettings_ShouldThrowArgumentException_WhenClientIdIsIncorrect(string clientId)
    {
        // Arrange
        var authenticationSecret = new AuthenticationSecret
        {
            ServerUrl = "https://example.com",
            ClientId = clientId,
            ClientSecret = "secret"
        };

        // Act
        var action = new Action(() => _sut.InitializeClientSettings(authenticationSecret));

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void InitializeClientSettings_ShouldThrowArgumentException_WhenClientSecretIsIncorrect(string clientSecret)
    {
        // Arrange
        var authenticationSecret = new AuthenticationSecret
        {
            ServerUrl = "https://example.com",
            ClientId = "123456",
            ClientSecret = clientSecret
        };

        // Act
        var action = new Action(() => _sut.InitializeClientSettings(authenticationSecret));

        // Assert
        action.Should().Throw<ArgumentException>();
    }
}