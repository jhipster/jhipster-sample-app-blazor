using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jhipster.Client.Models;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.PieceOfWork;
using Jhipster.Client.Services.EntityServices.Job;

using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Pages.Entities.PieceOfWork
{
    public partial class PieceOfWorkUpdate : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private IPieceOfWorkService PieceOfWorkService { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        public PieceOfWorkModel PieceOfWorkModel { get; set; } = new PieceOfWorkModel();

        protected override async Task OnInitializedAsync()
        {
            if (Id != 0)
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
            if (Id != 0)
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
