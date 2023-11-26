using System.Threading.Tasks;
using Jhipster.Client.Models;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.Region;
using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Pages.Entities.Region
{
    public partial class RegionDetail : ComponentBase
    {
        [Parameter]
        public long Id { get; set; }

        [Inject]
        public IRegionService RegionService { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public RegionModel Region { get; set; } = new RegionModel();

        protected override async Task OnInitializedAsync()
        {

            if (Id != 0)
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
