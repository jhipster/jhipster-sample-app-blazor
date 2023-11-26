using System.Net.Http;
using AutoMapper;
using Jhipster.Client.Models;
using Jhipster.Dto;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

namespace Jhipster.Client.Services.EntityServices.Region;

public class RegionService : AbstractEntityService<RegionModel, RegionDto, long>, IRegionService
{
    public RegionService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper, IConfiguration configuration)
        : base(httpClient, authenticationStateProvider, mapper, configuration, "/api/regions")
    {
    }
}
