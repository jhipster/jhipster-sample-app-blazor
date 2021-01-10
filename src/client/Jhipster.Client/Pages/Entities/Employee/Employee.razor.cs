using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using Jhipster.Client.Models;
using Jhipster.Client.Services.EntityServices.Employee;
using Jhipster.Client.Shared;
using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Pages.Entities.Employee
{
    public partial class Employee : ComponentBase
    {
        [Inject]
        private IEmployeeService EmployeeService { get; set; }

        [CascadingParameter]
        private IModalService ModalService { get; set; }

        private IList<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();

        protected override async Task OnInitializedAsync()
        {
            Employees = await EmployeeService.GetAll();
        }

        private async Task Delete(long employeesId)
        {
            var deleteModal = ModalService.Show<DeleteModal>("Confirm delete operation");
            var deleteResult = await deleteModal.Result;
            if (!deleteResult.Cancelled)
            {
                await EmployeeService.Delete(employeesId.ToString());
                Employees.Remove(Employees.First(employees => employees.Id.Equals(employeesId)));
            }
        }
    }
}
