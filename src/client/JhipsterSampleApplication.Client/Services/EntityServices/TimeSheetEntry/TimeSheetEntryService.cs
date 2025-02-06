using System;
using System.Net.Http;
using AutoMapper;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Dto;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

namespace JhipsterSampleApplication.Client.Services.EntityServices.TimeSheetEntry;

public class TimeSheetEntryService : AbstractEntityService<TimeSheetEntryModel, TimeSheetEntryDto, long?>, ITimeSheetEntryService
{
    public TimeSheetEntryService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper, IConfiguration configuration)
        : base(httpClient, authenticationStateProvider, mapper, configuration, "/api/time-sheet-entries")
    {
    }
}
