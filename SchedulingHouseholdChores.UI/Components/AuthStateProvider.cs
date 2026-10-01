using Microsoft.AspNetCore.Components;
using SchedulingHouseholdChores.UI.Services;

namespace SchedulingHouseholdChores.UI.Components;

public partial class AuthStateProvider : ComponentBase
{
    [Inject]
    private AuthClientService AuthService { get; set; } = null!;

    [Inject]
    private NavigationManager Navigation { get; set; } = null!;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private bool isAuthenticated = false;

    protected override async Task OnInitializedAsync()
    {
        isAuthenticated = await AuthService.IsAuthenticatedAsync();
    }

    public async Task LogoutAsync()
    {
        await AuthService.LogoutAsync();
        isAuthenticated = false;
        Navigation.NavigateTo("/login");
    }

    public bool IsAuthenticated => isAuthenticated;
}
