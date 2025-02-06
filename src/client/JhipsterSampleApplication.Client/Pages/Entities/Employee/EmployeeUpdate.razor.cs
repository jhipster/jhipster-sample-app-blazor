using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.Employee;
using JhipsterSampleApplication.Client.Services.EntityServices.Job;

using JhipsterSampleApplication.Client.Services.EntityServices.TimeSheet;

using JhipsterSampleApplication.Client.Services.EntityServices.Department;

using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.Employee
{
    public partial class EmployeeUpdate : ComponentBase
    {
        [Parameter]
        public long? Id { get; set; }

        [Inject]
        private IEmployeeService EmployeeService { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        [Inject]
        private IDepartmentService DepartmentService { get; set; }

        private IEnumerable<EmployeeModel> Managers { get; set; } = new List<EmployeeModel>();

        private IEnumerable<DepartmentModel> Departments { get; set; } = new List<DepartmentModel>();

        public EmployeeModel EmployeeModel { get; set; } = new EmployeeModel();

        public IEnumerable<long?> EmployeeIds { get; set; } = new List<long?>();

        public long? EmployeeId { get; set; }

        public IEnumerable<long?> DepartmentIds { get; set; } = new List<long?>();

        public long? DepartmentId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Managers = await EmployeeService.GetAll();
            EmployeeIds = Managers.Select(employee => employee.Id).ToList();
            Departments = await DepartmentService.GetAll();
            DepartmentIds = Departments.Select(department => department.Id).ToList();

            if (Id != 0 && Id != null)
            {
                EmployeeModel = await EmployeeService.Get(Id.ToString());
                EmployeeId = EmployeeModel.ManagerId;
                DepartmentId = EmployeeModel.DepartmentId;
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }

        private async Task Save()
        {
            EmployeeModel.ManagerId = Managers?.SingleOrDefault(employee => employee.Id.Equals(EmployeeId))?.Id;
            EmployeeModel.DepartmentId = Departments?.SingleOrDefault(department => department.Id.Equals(DepartmentId))?.Id;

            if (Id != 0 && Id != null)
            {
                await EmployeeService.Update(EmployeeModel);
            }
            else
            {
                await EmployeeService.Add(EmployeeModel);
            }
            NavigationService.Previous();
        }
    }
}
