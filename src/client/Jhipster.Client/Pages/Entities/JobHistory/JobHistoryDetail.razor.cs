using System.Threading.Tasks;
using Jhipster.Client.Models;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.JobHistory;
using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Pages.Entities.JobHistory
{
    public partial class JobHistoryDetail : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IJobHistoryService JobHistoryService { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public JobHistoryModel JobHistory { get; set; } = new JobHistoryModel();

        protected override async Task OnInitializedAsync()
        {
            if (Id != 0)
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
