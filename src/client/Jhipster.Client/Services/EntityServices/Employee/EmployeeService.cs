using System.Net.Http;
using AutoMapper;
using Jhipster.Client.Models;
using Jhipster.Dto;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

namespace Jhipster.Client.Services.EntityServices.Employee;

public class EmployeeService : AbstractEntityService<EmployeeModel, EmployeeDto, long>, IEmployeeService
{
    public EmployeeService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper, IConfiguration configuration)
        : base(httpClient, authenticationStateProvider, mapper, configuration, "/api/employees")
    {
    }
}
