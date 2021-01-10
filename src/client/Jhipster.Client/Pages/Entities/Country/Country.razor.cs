using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using Jhipster.Client.Models;
using Jhipster.Client.Services.EntityServices.Country;
using Jhipster.Client.Shared;
using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Pages.Entities.Country
{
    public partial class Country : ComponentBase
    {
        [Inject]
        private ICountryService CountryService { get; set; }

        [CascadingParameter]
        private IModalService ModalService { get; set; }

        private IList<CountryModel> Countries { get; set; } = new List<CountryModel>();

        protected override async Task OnInitializedAsync()
        {
            Countries = await CountryService.GetAll();
        }

        private async Task Delete(long countriesId)
        {
            var deleteModal = ModalService.Show<DeleteModal>("Confirm delete operation");
            var deleteResult = await deleteModal.Result;
            if (!deleteResult.Cancelled)
            {
                await CountryService.Delete(countriesId.ToString());
                Countries.Remove(Countries.First(countries => countries.Id.Equals(countriesId)));
            }
        }
    }
}
