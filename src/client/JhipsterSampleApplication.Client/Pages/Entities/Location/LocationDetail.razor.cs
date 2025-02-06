using System;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.Location;
using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.Location
{
    public partial class LocationDetail : ComponentBase
    {
        [Parameter]
        public long? Id { get; set; }

        [Inject]
        public ILocationService LocationService { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public LocationModel Location { get; set; } = new LocationModel();

        protected override async Task OnInitializedAsync()
        {
            if (Id != 0 && Id != null)
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
