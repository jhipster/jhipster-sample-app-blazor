using System.Threading.Tasks;
using Jhipster.Client.Models;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.Employee;
using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Pages.Entities.Employee
{
    public partial class EmployeeDetail : ComponentBase
    {
        [Parameter]
        public long Id { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public EmployeeModel Employee { get; set; } = new EmployeeModel();

        protected override async Task OnInitializedAsync()
        {

            if (Id != 0)
            {
                Employee = await EmployeeService.Get(Id.ToString());
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }
    }
}
