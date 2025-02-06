using System;
using System.Net.Http;
using AutoMapper;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Dto;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

namespace JhipsterSampleApplication.Client.Services.EntityServices.PieceOfWork;

public class PieceOfWorkService : AbstractEntityService<PieceOfWorkModel, PieceOfWorkDto, long?>, IPieceOfWorkService
{
    public PieceOfWorkService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper, IConfiguration configuration)
        : base(httpClient, authenticationStateProvider, mapper, configuration, "/api/piece-of-works")
    {
    }
}
