using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.Location;
using JhipsterSampleApplication.Client.Services.EntityServices.Country;

using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.Location
{
    public partial class LocationUpdate : ComponentBase
    {
        [Parameter]
        public long? Id { get; set; }

        [Inject]
        private ILocationService LocationService { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        [Inject]
        private ICountryService CountryService { get; set; }

        private IEnumerable<CountryModel> Countries { get; set; } = new List<CountryModel>();

        public LocationModel LocationModel { get; set; } = new LocationModel();

        public IEnumerable<long?> CountryIds { get; set; } = new List<long?>();

        public long? CountryId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Countries = await CountryService.GetAll();
            CountryIds = Countries.Select(country => country.Id).ToList();

            if (Id != 0 && Id != null)
            {
                LocationModel = await LocationService.Get(Id.ToString());
                CountryId = LocationModel.CountryId;
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }

        private async Task Save()
        {
            LocationModel.CountryId = Countries?.SingleOrDefault(country => country.Id.Equals(CountryId))?.Id;

            if (Id != 0 && Id != null)
            {
                await LocationService.Update(LocationModel);
            }
            else
            {
                await LocationService.Add(LocationModel);
            }
            NavigationService.Previous();
        }
    }
}
