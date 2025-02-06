using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.TimeSheet;
using JhipsterSampleApplication.Client.Services.EntityServices.TimeSheetEntry;

using JhipsterSampleApplication.Client.Services.EntityServices.Employee;

using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.TimeSheet
{
    public partial class TimeSheetUpdate : ComponentBase
    {
        [Parameter]
        public Guid? Id { get; set; }

        [Inject]
        private ITimeSheetService TimeSheetService { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        [Inject]
        private IEmployeeService EmployeeService { get; set; }

        private IEnumerable<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();

        public TimeSheetModel TimeSheetModel { get; set; } = new TimeSheetModel();

        public IEnumerable<long?> EmployeeIds { get; set; } = new List<long?>();

        public long? EmployeeId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employees = await EmployeeService.GetAll();
            EmployeeIds = Employees.Select(employee => employee.Id).ToList();
            if (Id != null)
            {
                TimeSheetModel = await TimeSheetService.Get(Id.ToString());
                EmployeeId = TimeSheetModel.EmployeeId;
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }

        private async Task Save()
        {
            TimeSheetModel.EmployeeId = Employees?.SingleOrDefault(employee => employee.Id.Equals(EmployeeId))?.Id;
            if (Id != null)
            {
                await TimeSheetService.Update(TimeSheetModel);
            }
            else
            {
                await TimeSheetService.Add(TimeSheetModel);
            }
            NavigationService.Previous();
        }
    }
}
