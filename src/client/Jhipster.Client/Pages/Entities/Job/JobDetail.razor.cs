using System.Threading.Tasks;
using Jhipster.Client.Models;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.Job;
using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Pages.Entities.Job
{
    public partial class JobDetail : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IJobService JobService { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public JobModel Job { get; set; } = new JobModel();

        protected override async Task OnInitializedAsync()
        {
            if (Id != 0)
            {
                Job = await JobService.Get(Id.ToString());
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }
    }
}
