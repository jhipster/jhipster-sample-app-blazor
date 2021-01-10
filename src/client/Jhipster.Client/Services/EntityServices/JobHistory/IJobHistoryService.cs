using System.Collections.Generic;
using System.Threading.Tasks;
using Jhipster.Client.Models;

namespace Jhipster.Client.Services.EntityServices.JobHistory
{
    public interface IJobHistoryService
    {
        public Task<IList<JobHistoryModel>> GetAll();

        public Task<JobHistoryModel> Get(string id);

        public Task Add(JobHistoryModel model);

        Task Update(JobHistoryModel model);

        Task Delete(string id);
    }
}
