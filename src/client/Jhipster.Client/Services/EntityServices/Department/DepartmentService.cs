using System.Net.Http;
using AutoMapper;
using Jhipster.Client.Models;
using Jhipster.Dto;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

namespace Jhipster.Client.Services.EntityServices.Department;

public class DepartmentService : AbstractEntityService<DepartmentModel, DepartmentDto, long>, IDepartmentService
{
    public DepartmentService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper, IConfiguration configuration)
        : base(httpClient, authenticationStateProvider, mapper, configuration, "/api/departments")
    {
    }
}
