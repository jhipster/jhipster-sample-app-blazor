using System.Threading.Tasks;
using Jhipster.Client.Models;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.Location;
using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Pages.Entities.Location
{
    public partial class LocationDetail : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public ILocationService LocationService { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public LocationModel Location { get; set; } = new LocationModel();

        protected override async Task OnInitializedAsync()
        {
            if (Id != 0)
            {
                Location = await LocationService.Get(Id.ToString());
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }
    }
}
