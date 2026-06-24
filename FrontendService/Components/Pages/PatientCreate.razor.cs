using FrontendService.Models;
using Microsoft.AspNetCore.Components;

namespace FrontendService.Components.Pages;

public partial class PatientCreate
{
    [Inject] private HttpClient Http { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private Patient patient = new();
    private string message = "";

    private async Task Create()
    {
        var response = await Http.PostAsJsonAsync("api/patient", patient);
        if (response.IsSuccessStatusCode)
            Nav.NavigateTo("/");
        else
            message = "Erreur lors de la création.";
    }
}