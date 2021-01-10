using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using Jhipster.Dto;
using Jhipster.Client.Models;

namespace Jhipster.Client.Services.AccountServices
{
    public class RegisterService : IRegisterService
    {
        private const string RegisterUrl = "/api/register";

        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public RegisterService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _httpClient.BaseAddress = new Uri(Configuration.BaseUri);
        }

        public async Task<HttpResponseMessage> Save(UserSaveModel registerModel)
        {
            var registerDto = _mapper.Map<ManagedUserDto>(registerModel);
            return await _httpClient.PostAsJsonAsync(RegisterUrl, registerDto);
        }
    }
}
