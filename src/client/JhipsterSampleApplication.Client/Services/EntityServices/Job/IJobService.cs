using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;

namespace JhipsterSampleApplication.Client.Services.EntityServices.Job
{
    public interface IJobService
    {
        public Task<IList<JobModel>> GetAll();

        public Task<JobModel> Get(string id);

        public Task Add(JobModel model);

        Task Update(JobModel model);

        Task Delete(string id);
    }
}
