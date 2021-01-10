using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jhipster.Client.Models;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.JobHistory;
using Jhipster.Client.Services.EntityServices.Job;
using Jhipster.Client.Services.EntityServices.Department;
using Jhipster.Client.Services.EntityServices.Employee;
using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Pages.Entities.JobHistory
{
    public partial class JobHistoryUpdate : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private IJobHistoryService JobHistoryService { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        [Inject]
        private IJobService JobService { get; set; }

        [Inject]
        private IDepartmentService DepartmentService { get; set; }

        [Inject]
        private IEmployeeService EmployeeService { get; set; }

        private IEnumerable<JobModel> Jobs { get; set; } = new List<JobModel>();

        private IEnumerable<DepartmentModel> Departments { get; set; } = new List<DepartmentModel>();

        private IEnumerable<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();

        public JobHistoryModel JobHistoryModel { get; set; } = new JobHistoryModel();

        public IEnumerable<long> JobIds { get; set; } = new List<long>();

        public long? JobId { get; set; }

        public IEnumerable<long> DepartmentIds { get; set; } = new List<long>();

        public long? DepartmentId { get; set; }

        public IEnumerable<long> EmployeeIds { get; set; } = new List<long>();

        public long? EmployeeId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Jobs = await JobService.GetAll();
            JobIds = Jobs.Select(job => job.Id).ToList();
            Departments = await DepartmentService.GetAll();
            DepartmentIds = Departments.Select(department => department.Id).ToList();
            Employees = await EmployeeService.GetAll();
            EmployeeIds = Employees.Select(employee => employee.Id).ToList();
            if (Id != 0)
            {
                JobHistoryModel = await JobHistoryService.Get(Id.ToString());
                JobId = JobHistoryModel.JobId;
                DepartmentId = JobHistoryModel.DepartmentId;
                EmployeeId = JobHistoryModel.EmployeeId;
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }

        private async Task Save()
        {
            JobHistoryModel.JobId = Jobs?.SingleOrDefault(job => job.Id.Equals(JobId))?.Id;
            JobHistoryModel.DepartmentId = Departments?.SingleOrDefault(department => department.Id.Equals(DepartmentId))?.Id;
            JobHistoryModel.EmployeeId = Employees?.SingleOrDefault(employee => employee.Id.Equals(EmployeeId))?.Id;
            if (Id != 0)
            {
                await JobHistoryService.Update(JobHistoryModel);
            }
            else
            {
                await JobHistoryService.Add(JobHistoryModel);
            }
            NavigationService.Previous();
        }
    }
}
