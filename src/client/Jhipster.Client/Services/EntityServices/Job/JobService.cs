using System.Net.Http;
using AutoMapper;
using Jhipster.Client.Models;
using Jhipster.Dto;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

namespace Jhipster.Client.Services.EntityServices.Job;

public class JobService : AbstractEntityService<JobModel, JobDto, long>, IJobService
{
    public JobService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper, IConfiguration configuration)
        : base(httpClient, authenticationStateProvider, mapper, configuration, "/api/jobs")
    {
    }
}
