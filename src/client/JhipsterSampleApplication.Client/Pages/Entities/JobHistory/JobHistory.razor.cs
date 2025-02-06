using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Services.EntityServices.JobHistory;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.JobHistory
{
    public partial class JobHistory : ComponentBase
    {
        [Inject]
        private IJobHistoryService JobHistoryService { get; set; }

        [CascadingParameter]
        private IModalService ModalService { get; set; }

        private IList<JobHistoryModel> JobHistories { get; set; } = new List<JobHistoryModel>();

        protected override async Task OnInitializedAsync()
        {
            JobHistories = await JobHistoryService.GetAll();
        }

        private async Task Delete(long? jobhistoriesId)
        {
            var deleteModal = ModalService.Show<DeleteModal>("Confirm delete operation");
            var deleteResult = await deleteModal.Result;
            if (!deleteResult.Cancelled)
            {
                await JobHistoryService.Delete(jobhistoriesId.ToString());
                JobHistories.Remove(JobHistories.First(jobhistories => jobhistories.Id.Equals(jobhistoriesId)));
            }
        }
    }
}
