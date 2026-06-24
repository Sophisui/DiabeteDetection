using FrontendService.Services;
using Microsoft.AspNetCore.Components;

namespace FrontendService.Components.Pages;

public partial class Login
{
    [Inject] private AuthService AuthService { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private string username = "";
    private string password = "";
    private string error = "";

    private bool isLoading = false;

    private async Task HandleLogin()
    {
        isLoading = true;
        StateHasChanged();

        try
        {
            var success = await AuthService.LoginAsync(username, password);
            if (success)
            {
                AuthService.SetAuthHeader();
                Nav.NavigateTo("/");
            }
            else
            {
                error = "Identifiants incorrects.";
            }
        }
        catch (Exception ex)
        {
            error = $"Erreur: {ex.Message}";
        }
        finally
        {
            isLoading = false;
            StateHasChanged();
        }
    }
}