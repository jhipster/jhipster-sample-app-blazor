using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Jhipster.Client.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jhipster.Client.Services.EntityServices
{
    public class AbstractEntityService<TModel, TDto> where TModel : class
                                                     where TDto : class
    {
        private const string AuthorizationHeader = "Authorization";
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly JsonSerializerOptions _options;

        protected readonly HttpClient _httpClient;
        protected readonly IMapper _mapper;

        protected JwtToken JwtToken { get; set; }
        protected string BaseUrl { get; }

        public AbstractEntityService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper, string baseUrl)
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
            _mapper = mapper;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
                }
            };
        }

        public virtual async Task<IList<TModel>> GetAll()
        {
            var resultsDto = await _httpClient.GetFromJsonAsync<IList<TDto>>(BaseUrl, _options);
            return _mapper.Map<IList<TModel>>(resultsDto);
        }

        public virtual async Task<TModel> Get(string id)
        {
            var resultDto = await _httpClient.GetFromJsonAsync<TModel>($"{BaseUrl}/{id}", _options);
            return _mapper.Map<TModel>(resultDto);
        }

        public virtual async Task Add(TModel model)
        {
            var dto = _mapper.Map<TDto>(model);
            await _httpClient.PostAsJsonAsync(BaseUrl, dto, _options);
        }

        public virtual async Task Update(TModel model)
        {
            var dto = _mapper.Map<TDto>(model);
            await _httpClient.PutAsJsonAsync(BaseUrl, dto, _options);
        }

        public virtual async Task Delete(string id)
        {
            await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        }

    }
}
