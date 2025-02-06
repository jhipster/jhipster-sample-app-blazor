using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;

namespace JhipsterSampleApplication.Client.Services.EntityServices.TimeSheet
{
    public interface ITimeSheetService
    {
        public Task<IList<TimeSheetModel>> GetAll();

        public Task<TimeSheetModel> Get(string id);

        public Task Add(TimeSheetModel model);

        Task Update(TimeSheetModel model);

        Task Delete(string id);
    }
}
