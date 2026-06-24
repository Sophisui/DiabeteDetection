using FrontendService.Models;
using FrontendService.Services;
using Microsoft.AspNetCore.Components;

namespace FrontendService.Components.Pages;

public partial class Home
{
    [Inject] private HttpClient Http { get; set; } = default!;
    [Inject] private AuthService AuthService { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private List<Patient>? patients;

    protected override async Task OnInitializedAsync()
    {
        if (!AuthService.IsAuthenticated)
        {
            Nav.NavigateTo("/login");
            return;
        }

        patients = await Http.GetFromJsonAsync<List<Patient>>("api/patient");
    }
}