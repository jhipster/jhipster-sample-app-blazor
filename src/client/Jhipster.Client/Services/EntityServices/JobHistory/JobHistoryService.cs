using System.Net.Http;
using AutoMapper;
using Jhipster.Client.Models;
using Jhipster.Dto;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jhipster.Client.Services.EntityServices.JobHistory
{
    public class JobHistoryService : AbstractEntityService<JobHistoryModel, JobHistoryDto>, IJobHistoryService
    {
        public JobHistoryService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper)
            : base(httpClient, authenticationStateProvider, mapper, "/api/job-histories")
        {
        }
    }
}
