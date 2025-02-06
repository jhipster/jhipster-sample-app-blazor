using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.TimeSheetEntry;
using JhipsterSampleApplication.Client.Services.EntityServices.TimeSheet;

using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.TimeSheetEntry
{
    public partial class TimeSheetEntryUpdate : ComponentBase
    {
        [Parameter]
        public long? Id { get; set; }

        [Inject]
        private ITimeSheetEntryService TimeSheetEntryService { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        [Inject]
        private ITimeSheetService TimeSheetService { get; set; }

        private IEnumerable<TimeSheetModel> TimeSheets { get; set; } = new List<TimeSheetModel>();

        public TimeSheetEntryModel TimeSheetEntryModel { get; set; } = new TimeSheetEntryModel();

        public IEnumerable<Guid?> TimeSheetIds { get; set; } = new List<Guid?>();

        public Guid? TimeSheetId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TimeSheets = await TimeSheetService.GetAll();
            TimeSheetIds = TimeSheets.Select(timesheet => timesheet.Id).ToList();

            if (Id != 0 && Id != null)
            {
                TimeSheetEntryModel = await TimeSheetEntryService.Get(Id.ToString());
                TimeSheetId = TimeSheetEntryModel.TimeSheetId;
            }
        }

        private void Back()
        {
            NavigationService.Previous();
        }

        private async Task Save()
        {
            TimeSheetEntryModel.TimeSheetId = TimeSheets?.SingleOrDefault(timesheet => timesheet.Id.Equals(TimeSheetId))?.Id;

            if (Id != 0 && Id != null)
            {
                await TimeSheetEntryService.Update(TimeSheetEntryModel);
            }
            else
            {
                await TimeSheetEntryService.Add(TimeSheetEntryModel);
            }
            NavigationService.Previous();
        }
    }
}
