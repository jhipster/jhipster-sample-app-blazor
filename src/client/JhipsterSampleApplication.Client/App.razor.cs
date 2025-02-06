using Microsoft.AspNetCore.Components;
using JhipsterSampleApplication.Client.Pages.Utils;

namespace JhipsterSampleApplication.Client
{
    public partial class App : ComponentBase
    {
        [Inject]
        public INavigationService NavigationService { get; set; } // Permit to initialize navigation service
    }
}
