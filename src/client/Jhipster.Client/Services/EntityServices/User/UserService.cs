using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Jhipster.Client.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jhipster.Client.Services.EntityServices.User
{
    public class UserService : AbstractEntityService<UserModel>,IUserService
    {
        public UserService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider) : base(httpClient, authenticationStateProvider, "/api/users")
        {
        }

        public async Task<IEnumerable<string>> GetAllAuthorities()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<string>>($"{BaseUrl}/authorities");
        }
    }
}
