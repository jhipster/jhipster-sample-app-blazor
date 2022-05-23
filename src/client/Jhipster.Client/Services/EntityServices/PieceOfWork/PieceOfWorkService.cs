using System.Net.Http;
using AutoMapper;
using Jhipster.Client.Models;
using Jhipster.Dto;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

namespace Jhipster.Client.Services.EntityServices.PieceOfWork
{
    public class PieceOfWorkService : AbstractEntityService<PieceOfWorkModel, PieceOfWorkDto, long>, IPieceOfWorkService
    {
        public PieceOfWorkService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper, IConfiguration configuration)
            : base(httpClient, authenticationStateProvider, mapper, configuration, "/api/piece-of-works")
        {
        }
    }
}
