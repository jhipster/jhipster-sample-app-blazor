using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;

namespace JhipsterSampleApplication.Client.Services.EntityServices.Department
{
    public interface IDepartmentService
    {
        public Task<IList<DepartmentModel>> GetAll();

        public Task<DepartmentModel> Get(string id);

        public Task Add(DepartmentModel model);

        Task Update(DepartmentModel model);

        Task Delete(string id);
    }
}
