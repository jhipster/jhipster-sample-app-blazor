using System.Net.Http;
using AutoMapper;
using Jhipster.Client.Models;
using Jhipster.Dto;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jhipster.Client.Services.EntityServices.Department
{
    public class DepartmentService : AbstractEntityService<DepartmentModel, DepartmentDto>, IDepartmentService
    {
        public DepartmentService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper)
            : base(httpClient, authenticationStateProvider, mapper, "/api/departments")
        {
        }
    }
}
