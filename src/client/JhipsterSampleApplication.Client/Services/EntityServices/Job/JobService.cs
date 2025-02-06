using System;
using System.Net.Http;
using AutoMapper;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Dto;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

namespace JhipsterSampleApplication.Client.Services.EntityServices.Job;

public class JobService : AbstractEntityService<JobModel, JobDto, long?>, IJobService
{
    public JobService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper, IConfiguration configuration)
        : base(httpClient, authenticationStateProvider, mapper, configuration, "/api/jobs")
    {
    }
}
