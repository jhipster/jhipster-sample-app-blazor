using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Services.EntityServices.PieceOfWork;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;

namespace JhipsterSampleApplication.Client.Pages.Entities.PieceOfWork
{
    public partial class PieceOfWork : ComponentBase
    {
        [Inject]
        private IPieceOfWorkService PieceOfWorkService { get; set; }

        [CascadingParameter]
        private IModalService ModalService { get; set; }

        private IList<PieceOfWorkModel> PieceOfWorks { get; set; } = new List<PieceOfWorkModel>();

        protected override async Task OnInitializedAsync()
        {
            PieceOfWorks = await PieceOfWorkService.GetAll();
        }

        private async Task Delete(long? pieceofworksId)
        {
            var deleteModal = ModalService.Show<DeleteModal>("Confirm delete operation");
            var deleteResult = await deleteModal.Result;
            if (!deleteResult.Cancelled)
            {
                await PieceOfWorkService.Delete(pieceofworksId.ToString());
                PieceOfWorks.Remove(PieceOfWorks.First(pieceofworks => pieceofworks.Id.Equals(pieceofworksId)));
            }
        }
    }
}
