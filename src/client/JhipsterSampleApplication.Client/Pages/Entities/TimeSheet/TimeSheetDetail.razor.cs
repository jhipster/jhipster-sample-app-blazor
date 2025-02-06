using System;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.TimeSheet;
using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.TimeSheet
{
    public partial class TimeSheetDetail : ComponentBase
    {
        [Parameter]
        public Guid? Id { get; set; }

        [Inject]
        public ITimeSheetService TimeSheetService { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public TimeSheetModel TimeSheet { get; set; } = new TimeSheetModel();

        protected override async Task OnInitializedAsync()
        {
            if (Id != null)
            {
                TimeSheet = await TimeSheetService.Get(Id.ToString());
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }
    }
}
