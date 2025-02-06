
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Services;
using JhipsterSampleApplication.Client.Services.EntityServices.User;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace JhipsterSampleApplication.Client.Pages.Admin.UserManagement
{
    public partial class UserManagement : ComponentBase
    {
        private IList<UserModel> UserModels { get; set; }

        [CascadingParameter]
        private IModalService ModalService { get; set; }

        [Inject]
        private IUserService UserService { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private IAuthenticationService AuthenticationService => AuthenticationStateProvider as IAuthenticationService;

        protected override async Task OnInitializedAsync()
        {
            UserModels = await UserService.GetAll();
        }

        private async Task ActiveUser(UserModel user, bool activated)
        {
            user.Activated = activated;
            await UserService.Update(user);
        }

        private async Task DeleteUser(string login)
        {
            var deleteModal = ModalService.Show<DeleteModal>("Confirm delete operation");
            var deleteResult = await deleteModal.Result;
            if (!deleteResult.Cancelled)
            {
                await UserService.Delete(login);
                UserModels.Remove(UserModels.First(user => user.Login.Equals(login)));
            }
        }
    }
}
