using System.Net.Http;
using AutoMapper;
using Jhipster.Client.Models;
using Jhipster.Dto;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jhipster.Client.Services.EntityServices.PieceOfWork
{
    public class PieceOfWorkService : AbstractEntityService<PieceOfWorkModel, PieceOfWorkDto>, IPieceOfWorkService
    {
        public PieceOfWorkService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IMapper mapper)
            : base(httpClient, authenticationStateProvider, mapper, "/api/piece-of-works")
        {
        }
    }
}
