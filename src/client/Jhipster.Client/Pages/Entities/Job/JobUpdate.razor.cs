using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jhipster.Client.Models;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.Job;
using Jhipster.Client.Services.EntityServices.PieceOfWork;

using Jhipster.Client.Services.EntityServices.Employee;

using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Pages.Entities.Job
{
    public partial class JobUpdate : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private IJobService JobService { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        [Inject]
        private IPieceOfWorkService PieceOfWorkService { get; set; }

        [Inject]
        private IEmployeeService EmployeeService { get; set; }

        private IEnumerable<PieceOfWorkModel> Chores { get; set; } = new List<PieceOfWorkModel>();

        private IEnumerable<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();

        public JobModel JobModel { get; set; } = new JobModel();

        public IReadOnlyList<long> PieceOfWorkIds { get; set; } = new List<long>();

        public IReadOnlyList<long> SelectedChores { get; set; }

        public IEnumerable<long> EmployeeIds { get; set; } = new List<long>();

        public long? EmployeeId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Chores = await PieceOfWorkService.GetAll();
            PieceOfWorkIds = Chores.Select(pieceofwork => pieceofwork.Id).ToList();
            Employees = await EmployeeService.GetAll();
            EmployeeIds = Employees.Select(employee => employee.Id).ToList();
            if (Id != 0)
            {
                JobModel = await JobService.Get(Id.ToString());
                SelectedChores = new List<long>(JobModel.Chores.Select(pieceofwork => pieceofwork.Id));
                EmployeeId = JobModel.EmployeeId;
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }

        private async Task Save()
        {
            if (SelectedChores != null)
            {
                JobModel.Chores = Chores?.Where(pieceofwork => SelectedChores.Contains(pieceofwork.Id)).ToList();
            }
            else
            {
                JobModel.Chores = null;
            }
            JobModel.EmployeeId = Employees?.SingleOrDefault(employee => employee.Id.Equals(EmployeeId))?.Id;
            if (Id != 0)
            {
                await JobService.Update(JobModel);
            }
            else
            {
                await JobService.Add(JobModel);
            }
            NavigationService.Previous();
        }
    }
}
