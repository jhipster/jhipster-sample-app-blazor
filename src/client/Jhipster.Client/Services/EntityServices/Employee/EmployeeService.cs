using System.Net.Http;
using AutoMapper;
using Jhipster.Client.Models;
using Jhipster.Dto;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jhipster.Client.Services.EntityServices.Employee
{
    public class EmployeeService : AbstractEntityService<EmployeeModel, EmployeeDto>, IEmployeeService
    {
        public EmployeeService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper)
            : base(httpClient, authenticationStateProvider, mapper, "/api/employees")
        {
        }
    }
}
