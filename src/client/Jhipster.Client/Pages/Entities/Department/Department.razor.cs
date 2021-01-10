using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using Jhipster.Client.Models;
using Jhipster.Client.Services.EntityServices.Department;
using Jhipster.Client.Shared;
using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Pages.Entities.Department
{
    public partial class Department : ComponentBase
    {
        [Inject]
        private IDepartmentService DepartmentService { get; set; }

        [CascadingParameter]
        private IModalService ModalService { get; set; }

        private IList<DepartmentModel> Departments { get; set; } = new List<DepartmentModel>();

        protected override async Task OnInitializedAsync()
        {
            Departments = await DepartmentService.GetAll();
        }

        private async Task Delete(long departmentsId)
        {
            var deleteModal = ModalService.Show<DeleteModal>("Confirm delete operation");
            var deleteResult = await deleteModal.Result;
            if (!deleteResult.Cancelled)
            {
                await DepartmentService.Delete(departmentsId.ToString());
                Departments.Remove(Departments.First(departments => departments.Id.Equals(departmentsId)));
            }
        }
    }
}
