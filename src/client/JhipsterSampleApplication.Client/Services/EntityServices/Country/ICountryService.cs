using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;

namespace JhipsterSampleApplication.Client.Services.EntityServices.Country
{
    public interface ICountryService
    {
        public Task<IList<CountryModel>> GetAll();

        public Task<CountryModel> Get(string id);

        public Task Add(CountryModel model);

        Task Update(CountryModel model);

        Task Delete(string id);
    }
}
