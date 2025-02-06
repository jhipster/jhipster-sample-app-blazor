using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using JhipsterSampleApplication.Dto;
using JhipsterSampleApplication.Client.Models;
using Microsoft.Extensions.Configuration;

namespace JhipsterSampleApplication.Client.Services.AccountServices;

public class RegisterService : IRegisterService
{
    private const string RegisterUrl = "/api/register";

    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly ConfigurationModel _configurationModel = new ConfigurationModel();

    public RegisterService(HttpClient httpClient, IMapper mapper, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        configuration.Bind(_configurationModel);
        _httpClient.BaseAddress = new Uri(_configurationModel.ServerUrl);
    }

    public async Task<HttpResponseMessage> Save(UserSaveModel registerModel)
    {
        var registerDto = _mapper.Map<ManagedUserDto>(registerModel);
        return await _httpClient.PostAsJsonAsync(RegisterUrl, registerDto);
    }
}
