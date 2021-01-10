using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jhipster.Client.Models;
using Jhipster.Dto;

namespace Jhipster.Client.Services.EntityServices.User
{
    public interface IUserService
    {
        public Task<IList<UserModel>> GetAll();

        public Task<UserModel> Get(string id);

        public Task Add(UserModel model);

        Task Update(UserModel model);

        Task Delete(string id);

        Task<IEnumerable<string>> GetAllAuthorities();
    }
}
