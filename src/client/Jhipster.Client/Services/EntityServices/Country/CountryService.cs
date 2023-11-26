using System.Net.Http;
using AutoMapper;
using Jhipster.Client.Models;
using Jhipster.Dto;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

namespace Jhipster.Client.Services.EntityServices.Country;

public class CountryService : AbstractEntityService<CountryModel, CountryDto, long>, ICountryService
{
    public CountryService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper, IConfiguration configuration)
        : base(httpClient, authenticationStateProvider, mapper, configuration, "/api/countries")
    {
    }
}
