using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using Blazored.Modal.Services;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Bunit;
using Bunit.Rendering;
using FluentAssertions;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages;
using JhipsterSampleApplication.Client.Pages.Admin.UserManagement;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.User;
using JhipsterSampleApplication.Client.Test.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Admin.UserManagement;

public class UserDetailTest : TestContext
{
    private readonly Mock<IUserService> _userService;
    private readonly Mock<INavigationService> _navidationService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public UserDetailTest()
    {
        _userService = new Mock<IUserService>();
        _navidationService = new Mock<INavigationService>();
        Services.AddBlazorise(options =>
            {
                options.Immediate = true;
            })
            .Replace(ServiceDescriptor.Transient<IComponentActivator, ComponentActivator>())
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
        Services.AddSingleton<IUserService>(_userService.Object);
        Services.AddSingleton<INavigationService>(_navidationService.Object);
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [Fact]
    public void Should_DisplayUserLogin_When_IdIsPresent()
    {
        //Arrange
        var user = _fixture.Create<UserModel>();
        _userService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(user));
        var userDetail = RenderComponent<UserDetail>(ComponentParameter.CreateParameter("Id", "test"));

        // Act
        var title = userDetail.Find("h2");

        // Assert
        title.MarkupMatches($"<h2><span>User</span> [<b>{user.Login}</b>]</h2>");

    }

    [Fact]
    public void Should_NotDisplayUser_When_IdIsNotPresent()
    {
        //Arrange
        _userService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(new UserModel()));
        var userDetail = RenderComponent<UserDetail>();

        // Act
        var title = userDetail.Find("div.col-8");

        // Assert
        title.Children.Length.Should().Be(0);

    }
}
