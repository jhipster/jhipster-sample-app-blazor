using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.Region;
using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.Region
{
    public partial class RegionUpdate : ComponentBase
    {
        [Parameter]
        public long? Id { get; set; }

        [Inject]
        private IRegionService RegionService { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        public RegionModel RegionModel { get; set; } = new RegionModel();

        protected override async Task OnInitializedAsync()
        {

            if (Id != 0 && Id != null)
            {
                RegionModel = await RegionService.Get(Id.ToString());
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
                await RegionService.Update(RegionModel);
            }
            else
            {
                await RegionService.Add(RegionModel);
            }
            NavigationService.Previous();
        }
    }
}
