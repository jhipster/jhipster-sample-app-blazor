using System.Threading.Tasks;
using Blazored.Modal.Services;
using JhipsterSampleApplication.Client.Pages;
using JhipsterSampleApplication.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace JhipsterSampleApplication.Client.Shared
{
    public partial class NavMenu
    {
        [Inject]
        private AuthenticationStateProvider AuthenticationService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        private IModalService ModalService { get; set; }

        private Task SignIn()
        {
            ModalService.Show<Login>("Sign In");
            return Task.CompletedTask;
        }

        private async Task SignOut()
        {
            await (AuthenticationService as IAuthenticationService).SignOut();
            NavigationManager.NavigateTo("/");
        }

        private void UserManagement()
        {
            NavigationManager.NavigateTo("/admin/user-management");
        }

    }
}
