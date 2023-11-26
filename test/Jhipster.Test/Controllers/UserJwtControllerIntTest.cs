using FluentAssertions;
using FluentAssertions.Json;
using Jhipster.Domain.Entities;
using Jhipster.Dto;
using Jhipster.Dto.Authentication;
using Jhipster.Test.Setup;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Jhipster.Test.Controllers;

public class UserJwtControllerIntTest
{
    public UserJwtControllerIntTest()
    {
        _factory = new AppWebApplicationFactory<TestStartup>();
        _client = _factory.CreateClient();

        _userManager = _factory.GetRequiredService<UserManager<User>>();
        _passwordHasher = _factory.GetRequiredService<IPasswordHasher<User>>();
    }

    private readonly AppWebApplicationFactory<TestStartup> _factory;
    private readonly HttpClient _client;

    private readonly UserManager<User> _userManager;
    private readonly IPasswordHasher<User> _passwordHasher;

    [Fact]
    public async Task TestAuthorize()
    {
        var user = new User
        {
            UserName = "user-jwt-controller",
            Email = "user-jwt-controller@example.com",
            PasswordHash = _passwordHasher.HashPassword(null, "test"),
            Activated = true
        };
        await _userManager.CreateAsync(user);

        var login = new LoginDto
        {
            Username = "user-jwt-controller",
            Password = "test"
        };

        var response = await _client.PostAsync("/api/authenticate", TestUtil.ToJsonContent(login));
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var json = JToken.Parse(await response.Content.ReadAsStringAsync());
        json.Should().HaveElement("id_token");
        json.SelectToken("$.id_token").Value<string>().Should().NotBeEmpty();

        var headers = response.Headers;
        headers.Contains("Authorization").Should().BeTrue();
        var authorization = headers.FirstOrDefault(it => it.Key == "Authorization").Value.FirstOrDefault();
        authorization.Should().NotBeNull().And.NotBeEmpty();
    }

    [Fact]
    public async Task TestAuthorizeFails()
    {
        var login = new LoginDto
        {
            Username = "wrong-user",
            Password = "wrong-password"
        };

        var response = await _client.PostAsync("/api/authenticate", TestUtil.ToJsonContent(login));
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var json = JToken.Parse(await response.Content.ReadAsStringAsync());
        json.Should().NotHaveElement("id_token");

        var headers = response.Headers;
        headers.Contains("Authorization").Should().BeFalse();
    }

    [Fact]
    public async Task TestAuthorizeWithRememberMe()
    {
        var user = new User
        {
            UserName = "user-jwt-controller",
            Email = "user-jwt-controller@example.com",
            PasswordHash = _passwordHasher.HashPassword(null, "test"),
            Activated = true
        };

        await _userManager.CreateAsync(user);

        var login = new LoginDto
        {
            Username = "user-jwt-controller",
            Password = "test",
            RememberMe = true
        };

        var response = await _client.PostAsync("/api/authenticate", TestUtil.ToJsonContent(login));
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var json = JToken.Parse(await response.Content.ReadAsStringAsync());
        json.Should().HaveElement("id_token");
        json.SelectToken("$.id_token").Value<string>().Should().NotBeEmpty();

        var headers = response.Headers;
        headers.Contains("Authorization").Should().BeTrue();
        var authorization = headers.FirstOrDefault(it => it.Key == "Authorization").Value.FirstOrDefault();
        authorization.Should().NotBeNull().And.NotBeEmpty();
    }
}
