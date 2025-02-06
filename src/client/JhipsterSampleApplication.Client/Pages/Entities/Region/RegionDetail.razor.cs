using System;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.Region;
using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.Region
{
    public partial class RegionDetail : ComponentBase
    {
        [Parameter]
        public long? Id { get; set; }

        [Inject]
        public IRegionService RegionService { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public RegionModel Region { get; set; } = new RegionModel();

        protected override async Task OnInitializedAsync()
        {
            if (Id != 0 && Id != null)
            {
                Region = await RegionService.Get(Id.ToString());
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }
    }
}
