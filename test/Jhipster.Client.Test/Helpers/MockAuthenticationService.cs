using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Jhipster.Client.Models;
using Jhipster.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jhipster.Client.Test.Helpers
{
    public class MockAuthenticationService : AuthenticationStateProvider,IAuthenticationService
    {
        public ClaimsIdentity Identity { get; set; }

        public MockAuthenticationService()
        {
            
        }

        public MockAuthenticationService(ClaimsIdentity identity)
        {
            Identity = identity;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(Identity)));
        }

        public bool IsAuthenticated
        {
            get => true;
            set => throw new NotImplementedException();
        }

        public UserModel CurrentUser
        {
            get => new UserModel(){Login = Identity?.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Value};
            set => throw new NotImplementedException();
        }

        public JwtToken JwtToken
        {
            get => new JwtToken();
            set => throw new NotImplementedException();
        }

        public virtual async Task<bool> SignIn(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public virtual async Task SignOut()
        {
            throw new NotImplementedException();
        }
    }
}
