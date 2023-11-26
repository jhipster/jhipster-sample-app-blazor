using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jhipster.Client.Models;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.Region;
using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Pages.Entities.Region
{
    public partial class RegionUpdate : ComponentBase
    {
        [Parameter]
        public long Id { get; set; }

        [Inject]
        private IRegionService RegionService { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        public RegionModel RegionModel { get; set; } = new RegionModel();

        protected override async Task OnInitializedAsync()
        {

            if (Id != 0)
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

            if (Id != 0)
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
