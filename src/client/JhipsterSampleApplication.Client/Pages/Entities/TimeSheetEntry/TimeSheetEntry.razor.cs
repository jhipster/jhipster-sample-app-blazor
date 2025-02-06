using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Services.EntityServices.TimeSheetEntry;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.TimeSheetEntry
{
    public partial class TimeSheetEntry : ComponentBase
    {
        [Inject]
        private ITimeSheetEntryService TimeSheetEntryService { get; set; }

        [CascadingParameter]
        private IModalService ModalService { get; set; }

        private IList<TimeSheetEntryModel> TimeSheetEntries { get; set; } = new List<TimeSheetEntryModel>();

        protected override async Task OnInitializedAsync()
        {
            TimeSheetEntries = await TimeSheetEntryService.GetAll();
        }

        private async Task Delete(long? timesheetentriesId)
        {
            var deleteModal = ModalService.Show<DeleteModal>("Confirm delete operation");
            var deleteResult = await deleteModal.Result;
            if (!deleteResult.Cancelled)
            {
                await TimeSheetEntryService.Delete(timesheetentriesId.ToString());
                TimeSheetEntries.Remove(TimeSheetEntries.First(timesheetentries => timesheetentries.Id.Equals(timesheetentriesId)));
            }
        }
    }
}
