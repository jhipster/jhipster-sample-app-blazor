using System;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.TimeSheetEntry;
using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.TimeSheetEntry
{
    public partial class TimeSheetEntryDetail : ComponentBase
    {
        [Parameter]
        public long? Id { get; set; }

        [Inject]
        public ITimeSheetEntryService TimeSheetEntryService { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public TimeSheetEntryModel TimeSheetEntry { get; set; } = new TimeSheetEntryModel();

        protected override async Task OnInitializedAsync()
        {
            if (Id != 0 && Id != null)
            {
                TimeSheetEntry = await TimeSheetEntryService.Get(Id.ToString());
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }
    }
}
