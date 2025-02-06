using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services;
using JhipsterSampleApplication.Client.Services.EntityServices.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace JhipsterSampleApplication.Client.Pages.Admin.UserManagement
{
    public partial class UserUpdate : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }

        [Inject]
        private IUserService UserService { get; set; }

        [Inject]
        private INavigationService NavigationService { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private IAuthenticationService AuthenticationService => AuthenticationStateProvider as IAuthenticationService;

        private UserModel CurrentUser { get; set; }

        private IEnumerable<string> Authorities { get; set; } = new List<string>();

        private IEnumerable<string> SelectedAuthorities { get; set; } = new List<string>();
        private IReadOnlyList<string> CurrentAuthorities { get; set; } = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            CurrentUser = null;
            if (!string.IsNullOrWhiteSpace(Id))
            {
                CurrentUser = await UserService.Get(Id);
                SelectedAuthorities = CurrentUser.Authorities;
                CurrentAuthorities = new ReadOnlyCollection<string>(CurrentUser.Authorities.ToList());
            }
            else
            {
                CurrentUser = new UserModel();
            }

            Authorities = await UserService.GetAllAuthorities();
        }

        private void OnSelectAuthoritiesChanged(IReadOnlyList<string> selectedAuthorities)
        {
            SelectedAuthorities = selectedAuthorities;
            CurrentAuthorities = new ReadOnlyCollection<string>(SelectedAuthorities.ToList());
        }

        private async Task Save()
        {
            CurrentUser.Authorities = SelectedAuthorities;
            if (!string.IsNullOrWhiteSpace(Id))
            {
                await UserService.Update(CurrentUser);
            }
            else
            {
                await UserService.Add(CurrentUser);
            }
            NavigationService.Previous();
        }

        private void Back()
        {
            NavigationService.Previous();
        }
    }
}
