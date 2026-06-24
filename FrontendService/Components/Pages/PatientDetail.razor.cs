using FrontendService.Models;
using FrontendService.Services;
using Microsoft.AspNetCore.Components;

namespace FrontendService.Components.Pages;

public partial class PatientDetail
{
    [Parameter] public int Id { get; set; }
    [Inject] private HttpClient Http { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;
    [Inject] private AuthService AuthService { get; set; } = default!;

    private Patient? patient;
    private List<Note>? notes;
    private string message = "";
    private RiskAssessment? risk;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;

        await AuthService.InitializeAsync();
        patient = await Http.GetFromJsonAsync<Patient>($"api/patient/{Id}");
        notes = await Http.GetFromJsonAsync<List<Note>>($"api/note/patient/{Id}");
        risk = await Http.GetFromJsonAsync<RiskAssessment>($"api/risk/{Id}");
        StateHasChanged();
    }

    private async Task Update()
    {
        var response = await Http.PutAsJsonAsync($"api/patient/{Id}", patient);
        message = response.IsSuccessStatusCode ? "Patient modifié !" : "Erreur lors de la modification.";
    }

    private async Task Delete()
    {
        var response = await Http.DeleteAsync($"api/patient/{Id}");
        if (response.IsSuccessStatusCode)
            Nav.NavigateTo("/");
        else
            message = "Erreur lors de la suppression.";
    }

    private async Task AddNote(string content)
    {
        var note = new NoteCreateDto
        {
            PatientId = Id,
            PatientName = $"{patient?.FirstName} {patient?.LastName}",
            Content = content
        };
        var response = await Http.PostAsJsonAsync("api/note", note);
        if (response.IsSuccessStatusCode)
        {
            var result = await Http.GetAsync($"api/note/patient/{Id}");
            if (result.IsSuccessStatusCode)
            {
                notes = await result.Content.ReadFromJsonAsync<List<Note>>();
                StateHasChanged();
            }
        }
    }
}