using System.Threading.Tasks;
using Jhipster.Client.Models;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.Country;
using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Pages.Entities.Country
{
    public partial class CountryDetail : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public ICountryService CountryService { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public CountryModel Country { get; set; } = new CountryModel();

        protected override async Task OnInitializedAsync()
        {
            if (Id != 0)
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
