using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.Country;
using JhipsterSampleApplication.Client.Services.EntityServices.Region;

using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.Country
{
    public partial class CountryUpdate : ComponentBase
    {
        [Parameter]
        public long? Id { get; set; }

        [Inject]
        private ICountryService CountryService { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        [Inject]
        private IRegionService RegionService { get; set; }

        private IEnumerable<RegionModel> Regions { get; set; } = new List<RegionModel>();

        public CountryModel CountryModel { get; set; } = new CountryModel();

        public IEnumerable<long?> RegionIds { get; set; } = new List<long?>();

        public long? RegionId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Regions = await RegionService.GetAll();
            RegionIds = Regions.Select(region => region.Id).ToList();

            if (Id != 0 && Id != null)
            {
                CountryModel = await CountryService.Get(Id.ToString());
                RegionId = CountryModel.RegionId;
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }

        private async Task Save()
        {
            CountryModel.RegionId = Regions?.SingleOrDefault(region => region.Id.Equals(RegionId))?.Id;

            if (Id != 0 && Id != null)
            {
                await CountryService.Update(CountryModel);
            }
            else
            {
                await CountryService.Add(CountryModel);
            }
            NavigationService.Previous();
        }
    }
}
