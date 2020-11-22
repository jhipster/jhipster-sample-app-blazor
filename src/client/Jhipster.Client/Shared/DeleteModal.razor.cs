using System.Threading.Tasks;
using Blazored.Modal;
using Microsoft.AspNetCore.Components;

namespace Jhipster.Client.Shared
{
    public partial class DeleteModal
    {
        [CascadingParameter]
        BlazoredModalInstance BlazoredModal { get; set; }
        
        private async Task Delete()
        {
            await BlazoredModal.Close();
        }
        
        private async Task Close()
        {
            await BlazoredModal.Cancel();
        }
    }
}
