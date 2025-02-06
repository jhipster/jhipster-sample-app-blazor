using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Services.EntityServices.TimeSheet;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.TimeSheet
{
    public partial class TimeSheet : ComponentBase
    {
        [Inject]
        private ITimeSheetService TimeSheetService { get; set; }

        [CascadingParameter]
        private IModalService ModalService { get; set; }

        private IList<TimeSheetModel> TimeSheets { get; set; } = new List<TimeSheetModel>();

        protected override async Task OnInitializedAsync()
        {
            TimeSheets = await TimeSheetService.GetAll();
        }

        private async Task Delete(Guid? timesheetsId)
        {
            var deleteModal = ModalService.Show<DeleteModal>("Confirm delete operation");
            var deleteResult = await deleteModal.Result;
            if (!deleteResult.Cancelled)
            {
                await TimeSheetService.Delete(timesheetsId.ToString());
                TimeSheets.Remove(TimeSheets.First(timesheets => timesheets.Id.Equals(timesheetsId)));
            }
        }
    }
}
