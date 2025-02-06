using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;

namespace JhipsterSampleApplication.Client.Services.EntityServices.TimeSheetEntry
{
    public interface ITimeSheetEntryService
    {
        public Task<IList<TimeSheetEntryModel>> GetAll();

        public Task<TimeSheetEntryModel> Get(string id);

        public Task Add(TimeSheetEntryModel model);

        Task Update(TimeSheetEntryModel model);

        Task Delete(string id);
    }
}
