using System.Threading.Tasks;
using Blazored.Modal.Services;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace JhipsterSampleApplication.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private AuthenticationStateProvider AuthenticationService { get; set; }

        [CascadingParameter]
        private IModalService ModalService { get; set; }

        private UserModel CurrentUser => (AuthenticationService as IAuthenticationService)?.CurrentUser;

        private Task SignIn()
        {
            ModalService.Show<Login>("Sign In");
            return Task.CompletedTask;
        }

    }
}
