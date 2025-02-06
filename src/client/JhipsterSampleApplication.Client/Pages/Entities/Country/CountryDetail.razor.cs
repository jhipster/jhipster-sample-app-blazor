using System;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.Country;
using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.Country
{
    public partial class CountryDetail : ComponentBase
    {
        [Parameter]
        public long? Id { get; set; }

        [Inject]
        public ICountryService CountryService { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public CountryModel Country { get; set; } = new CountryModel();

        protected override async Task OnInitializedAsync()
        {
            if (Id != 0 && Id != null)
            {
                Country = await CountryService.Get(Id.ToString());
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }
    }
}
