using System.Collections.Generic;
using System.Threading.Tasks;
using Jhipster.Client.Models;

namespace Jhipster.Client.Services.EntityServices.PieceOfWork
{
    public interface IPieceOfWorkService
    {
        public Task<IList<PieceOfWorkModel>> GetAll();

        public Task<PieceOfWorkModel> Get(string id);

        public Task Add(PieceOfWorkModel model);

        Task Update(PieceOfWorkModel model);

        Task Delete(string id);
    }
}
