using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;

namespace JhipsterSampleApplication.Client.Services.EntityServices.Location
{
    public interface ILocationService
    {
        public Task<IList<LocationModel>> GetAll();

        public Task<LocationModel> Get(string id);

        public Task Add(LocationModel model);

        Task Update(LocationModel model);

        Task Delete(string id);
    }
}
