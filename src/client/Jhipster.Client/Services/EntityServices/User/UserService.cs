using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using Jhipster.Client.Models;
using Jhipster.Dto;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

namespace Jhipster.Client.Services.EntityServices.User
{
    public class UserService : AbstractEntityService<UserModel, UserDto, string>, IUserService
    {
        public UserService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper, IConfiguration configuration) : base(httpClient, authenticationStateProvider, mapper, configuration, "/api/admin/users")
        {
        }

        public async Task<IEnumerable<string>> GetAllAuthorities()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<string>>($"{BaseUrl}/authorities");
        }
    }
}
