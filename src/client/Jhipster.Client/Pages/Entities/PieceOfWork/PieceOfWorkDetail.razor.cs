using System.Threading.Tasks;
using Jhipster.Client.Models;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.PieceOfWork;
using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Pages.Entities.PieceOfWork
{
    public partial class PieceOfWorkDetail : ComponentBase
    {
        [Parameter]
        public long Id { get; set; }

        [Inject]
        public IPieceOfWorkService PieceOfWorkService { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public PieceOfWorkModel PieceOfWork { get; set; } = new PieceOfWorkModel();

        protected override async Task OnInitializedAsync()
        {

            if (Id != 0)
            {
                PieceOfWork = await PieceOfWorkService.Get(Id.ToString());
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }
    }
}
