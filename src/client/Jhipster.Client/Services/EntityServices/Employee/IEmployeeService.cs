using System.Collections.Generic;
using System.Threading.Tasks;
using Jhipster.Client.Models;

namespace Jhipster.Client.Services.EntityServices.Employee
{
    public interface IEmployeeService
    {
        public Task<IList<EmployeeModel>> GetAll();

        public Task<EmployeeModel> Get(string id);

        public Task Add(EmployeeModel model);

        Task Update(EmployeeModel model);

        Task Delete(string id);
    }
}
