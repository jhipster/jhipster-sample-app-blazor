using System.Threading.Tasks;
using Blazored.Modal.Services;
using Jhipster.Client.Models;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jhipster.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private AuthenticationStateProvider AuthenticationService { get; set; }

        [CascadingParameter]
        private IModalService ModalService { get; set; }

        private UserModel CurrentUser => (AuthenticationService as IAuthenticationService)?.CurrentUser;

        private async Task SignIn()
        {
            ModalService.Show<Login>("Sign In");
        }

    }
}
