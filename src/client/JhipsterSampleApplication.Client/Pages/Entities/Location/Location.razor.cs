using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Services.EntityServices.Location;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.Location
{
    public partial class Location : ComponentBase
    {
        [Inject]
        private ILocationService LocationService { get; set; }

        [CascadingParameter]
        private IModalService ModalService { get; set; }

        private IList<LocationModel> Locations { get; set; } = new List<LocationModel>();

        protected override async Task OnInitializedAsync()
        {
            Locations = await LocationService.GetAll();
        }

        private async Task Delete(long? locationsId)
        {
            var deleteModal = ModalService.Show<DeleteModal>("Confirm delete operation");
            var deleteResult = await deleteModal.Result;
            if (!deleteResult.Cancelled)
            {
                await LocationService.Delete(locationsId.ToString());
                Locations.Remove(Locations.First(locations => locations.Id.Equals(locationsId)));
            }
        }
    }
}
