using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using Jhipster.Client.Models;
using Jhipster.Client.Services.EntityServices.Region;
using Jhipster.Client.Shared;
using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Pages.Entities.Region
{
    public partial class Region : ComponentBase
    {
        [Inject]
        private IRegionService RegionService { get; set; }

        [CascadingParameter]
        private IModalService ModalService { get; set; }

        private IList<RegionModel> Regions { get; set; } = new List<RegionModel>();

        protected override async Task OnInitializedAsync()
        {
            Regions = await RegionService.GetAll();
        }

        private async Task Delete(long regionsId)
        {
            var deleteModal = ModalService.Show<DeleteModal>("Confirm delete operation");
            var deleteResult = await deleteModal.Result;
            if (!deleteResult.Cancelled)
            {
                await RegionService.Delete(regionsId.ToString());
                Regions.Remove(Regions.First(regions => regions.Id.Equals(regionsId)));
            }
        }
    }
}
