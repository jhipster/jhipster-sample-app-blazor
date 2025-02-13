using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Services.EntityServices.Job;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.Job
{
    public partial class Job : ComponentBase
    {
        [Inject]
        private IJobService JobService { get; set; }

        [CascadingParameter]
        private IModalService ModalService { get; set; }

        private IList<JobModel> Jobs { get; set; } = new List<JobModel>();

        protected override async Task OnInitializedAsync()
        {
            Jobs = await JobService.GetAll();
        }

        private async Task Delete(long? jobsId)
        {
            var deleteModal = ModalService.Show<DeleteModal>("Confirm delete operation");
            var deleteResult = await deleteModal.Result;
            if (!deleteResult.Cancelled)
            {
                await JobService.Delete(jobsId.ToString());
                Jobs.Remove(Jobs.First(jobs => jobs.Id.Equals(jobsId)));
            }
        }
    }
}
