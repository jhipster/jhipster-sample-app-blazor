using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.PieceOfWork;
using JhipsterSampleApplication.Client.Services.EntityServices.Job;

using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.PieceOfWork
{
    public partial class PieceOfWorkUpdate : ComponentBase
    {
        [Parameter]
        public long? Id { get; set; }

        [Inject]
        private IPieceOfWorkService PieceOfWorkService { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        public PieceOfWorkModel PieceOfWorkModel { get; set; } = new PieceOfWorkModel();

        protected override async Task OnInitializedAsync()
        {

            if (Id != 0 && Id != null)
            {
                PieceOfWorkModel = await PieceOfWorkService.Get(Id.ToString());
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }

        private async Task Save()
        {

            if (Id != 0 && Id != null)
            {
                await PieceOfWorkService.Update(PieceOfWorkModel);
            }
            else
            {
                await PieceOfWorkService.Add(PieceOfWorkModel);
            }
            NavigationService.Previous();
        }
    }
}
