using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace JhipsterSampleApplication.Client.Test.Helpers
{
    public static class AuthorizationHelper
    {
        public static IServiceCollection AddMockUnAuthenticateAuthorization(this IServiceCollection services)
        {
            var authenticationService = new Mock<AuthenticationStateProvider>();
            authenticationService.Setup(auth => auth.GetAuthenticationStateAsync())
                .Returns(() => Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
            services.AddScoped<IAuthorizationService, MockAuthorizationService>();
            services.AddScoped<IAuthorizationPolicyProvider, MockAuthorizationPolicyProvider>();
            services.AddSingleton<AuthenticationStateProvider>(authenticationService.Object);
            services.AddScoped<IAuthorizationHandlerProvider, DefaultAuthorizationHandlerProvider>();
            return services;
        }


        public static IServiceCollection AddMockAuthenticatedAuthorization(this IServiceCollection services, ClaimsIdentity identity)
        {
            var authenticationService = new MockAuthenticationService(identity);
            services.AddScoped<IAuthorizationService, MockAuthorizationService>();
            services.AddScoped<IAuthorizationPolicyProvider, MockAuthorizationPolicyProvider>();
            services.AddSingleton<AuthenticationStateProvider>(authenticationService);
            services.AddScoped<IAuthorizationHandlerProvider, DefaultAuthorizationHandlerProvider>();
            return services;
        }
    }
}
