using System.Net.Http;
using AutoMapper;
using Jhipster.Client.Models;
using Jhipster.Dto;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jhipster.Client.Services.EntityServices.Job
{
    public class JobService : AbstractEntityService<JobModel, JobDto>, IJobService
    {
        public JobService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper)
            : base(httpClient, authenticationStateProvider, mapper, "/api/jobs")
        {
        }
    }
}
