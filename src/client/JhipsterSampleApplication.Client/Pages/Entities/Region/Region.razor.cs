using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Services.EntityServices.Region;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.Region
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

        private async Task Delete(long? regionsId)
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
