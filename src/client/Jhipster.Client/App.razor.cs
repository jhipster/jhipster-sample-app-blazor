using Jhipster.Client.Pages.Utils;
using Microsoft.AspNetCore.Components;

namespace Jhipster.Client
{
    public partial class App
    {
        [Inject]
        public INavigationService NavigationService { get; set; } // Permit to initialize navigation service
    }
}
