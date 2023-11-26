using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Jhipster.Client.Pages.Utils;

public class NavigationService : INavigationService
{
    private readonly NavigationManager _navigationManagerBase;

    private Stack<string> PreviousStack { get; } = new Stack<string>();

    private Stack<string> NextStack { get; } = new Stack<string>();

    public NavigationService(NavigationManager navigationManagerBase)
    {
        _navigationManagerBase = navigationManagerBase;
        _navigationManagerBase.LocationChanged += NavigationManagerBaseOnLocationChanged;
    }

    public void Previous()
    {
        if (PreviousStack.Count <= 1) return;

        NextStack.Push(PreviousStack.Pop()); //Add Current location to next stack
        _navigationManagerBase.NavigateTo(PreviousStack.Pop());
    }

    public void Next()
    {
        _navigationManagerBase.NavigateTo(NextStack.Pop());
    }

    private void NavigationManagerBaseOnLocationChanged(object sender, LocationChangedEventArgs e)
    {
        PreviousStack.Push(e.Location);
    }
}
