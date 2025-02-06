using System;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.JobHistory;
using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.JobHistory
{
    public partial class JobHistoryDetail : ComponentBase
    {
        [Parameter]
        public long? Id { get; set; }

        [Inject]
        public IJobHistoryService JobHistoryService { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public JobHistoryModel JobHistory { get; set; } = new JobHistoryModel();

        protected override async Task OnInitializedAsync()
        {
            if (Id != 0 && Id != null)
            {
                JobHistory = await JobHistoryService.Get(Id.ToString());
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }
    }
}
