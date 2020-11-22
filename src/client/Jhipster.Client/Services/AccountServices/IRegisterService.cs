using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Jhipster.Client.Models;

namespace Jhipster.Client.Services.AccountServices
{
    public interface IRegisterService
    {
        Task<HttpResponseMessage> Save(UserSaveModel registerModel);
    }
}
