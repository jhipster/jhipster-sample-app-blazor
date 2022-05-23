using System.Net.Http;
using AutoMapper;
using Jhipster.Client.Models;
using Jhipster.Dto;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

namespace Jhipster.Client.Services.EntityServices.JobHistory
{
    public class JobHistoryService : AbstractEntityService<JobHistoryModel, JobHistoryDto, long>, IJobHistoryService
    {
        public JobHistoryService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper, IConfiguration configuration)
            : base(httpClient, authenticationStateProvider, mapper, configuration, "/api/job-histories")
        {
        }
    }
}
