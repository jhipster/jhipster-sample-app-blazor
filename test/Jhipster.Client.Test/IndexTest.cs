using System.Collections.Generic;
using System.Security.Claims;
using Blazored.Modal.Services;
using Bunit;
using Jhipster.Client.Pages;
using Jhipster.Client.Test.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Jhipster.Client.Test
{
    public class IndexTest : TestContext
    {
        [Fact]
        public void Should_CallSignInMethod_When_SignInWasClick()
        {
            //Arrange
            var modalService = new Mock<IModalService>();
            Services.AddSingleton<IModalService>(modalService.Object);
            Services.AddMockUnAuthenticateAuthorization();
            var authenticationStateProvider = Services.GetService<AuthenticationStateProvider>();

            var index = RenderComponent<Index>(ComponentParameterFactory.CascadingValue(authenticationStateProvider.GetAuthenticationStateAsync()), ComponentParameterFactory.CascadingValue(modalService.Object));

            // Act
            index.Find(".alert-link").Click();

            // Assert
            modalService.Verify(mock => mock.Show<Login>(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void Should_DisplayUserLogin_When_UserIsAuthenticated()
        {
            //Arrange
            var modalService = new Mock<IModalService>();
            Services.AddSingleton<IModalService>(modalService.Object);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, "UserTestLogin"));
            var claimIdentity = new ClaimsIdentity(claims);
            Services.AddMockAuthenticatedAuthorization(claimIdentity);
            var authenticationStateProvider = Services.GetService<AuthenticationStateProvider>();

            var index = RenderComponent<Index>(ComponentParameterFactory.CascadingValue(authenticationStateProvider.GetAuthenticationStateAsync()));

            // Act
            var homeLoggedMessage = index.Find("#home-logged-message");

            // Assert
            homeLoggedMessage.MarkupMatches(@"<span id=""home-logged-message"">You are logged in as user ""UserTestLogin"".</span>");
        }
    }
}
