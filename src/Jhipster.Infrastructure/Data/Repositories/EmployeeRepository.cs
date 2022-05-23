using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JHipsterNet.Core.Pagination;
using JHipsterNet.Core.Pagination.Extensions;
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using Jhipster.Infrastructure.Data.Extensions;

namespace Jhipster.Infrastructure.Data.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee, long>, IEmployeeRepository
    {
        public EmployeeRepository(IUnitOfWork context) : base(context)
        {
        }

        public override async Task<Employee> CreateOrUpdateAsync(Employee employee)
        {
            List<Type> entitiesToBeUpdated = new List<Type>();
            return await base.CreateOrUpdateAsync(employee, entitiesToBeUpdated);
        }
    }
}
