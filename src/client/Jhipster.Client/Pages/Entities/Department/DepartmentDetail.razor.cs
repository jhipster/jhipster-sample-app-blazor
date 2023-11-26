using System.Threading.Tasks;
using Jhipster.Client.Models;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.Department;
using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Pages.Entities.Department
{
    public partial class DepartmentDetail : ComponentBase
    {
        [Parameter]
        public long Id { get; set; }

        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public DepartmentModel Department { get; set; } = new DepartmentModel();

        protected override async Task OnInitializedAsync()
        {

            if (Id != 0)
            {
                Department = await DepartmentService.Get(Id.ToString());
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }
    }
}
