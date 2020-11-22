using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Jhipster.Client.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jhipster.Client.Services.EntityServices
{
    public class AbstractEntityService<T> where T : class
    {
        private const string AuthorizationHeader = "Authorization";
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        protected readonly HttpClient _httpClient;

        protected JwtToken JwtToken { get; set; }
        protected string BaseUrl { get; }

        public AbstractEntityService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, string baseUrl)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _httpClient.BaseAddress = new Uri(Configuration.BaseUri);
            var authenticationService = _authenticationStateProvider as IAuthenticationService;
            JwtToken = authenticationService?.JwtToken;
            if (JwtToken != null)
            {
                _httpClient.DefaultRequestHeaders.Add(AuthorizationHeader, $"Bearer {JwtToken.IdToken}");
            }
            BaseUrl = baseUrl;
        }

        public virtual async Task<IList<T>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<IList<T>>(BaseUrl);
        }

        public virtual async Task<T> Get(string id)
        {
            return await _httpClient.GetFromJsonAsync<T>($"{BaseUrl}/{id}");
        }

        public virtual async Task Add(T model)
        {
            await _httpClient.PostAsJsonAsync(BaseUrl,model);
        }
        
        public virtual async Task Update(T model)
        {
            await _httpClient.PutAsJsonAsync(BaseUrl, model);
        }

        public virtual async Task Delete(string id)
        {
            await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        }

    }
}
