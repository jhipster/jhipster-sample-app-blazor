using Microsoft.AspNetCore.Components;
using Jhipster.Client.Pages.Utils;

namespace Jhipster.Client
{
    public partial class App : ComponentBase
    {
        [Inject]
        public INavigationService NavigationService { get; set; } // Permit to initialize navigation service
    }
}
