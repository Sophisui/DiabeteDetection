using Microsoft.AspNetCore.Components;

namespace FrontendService.Components.Shared;

public partial class NoteForm
{
    [Parameter, EditorRequired]
    public EventCallback<string> OnNoteAdded { get; set; }

    public string Content { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    private async Task HandleSubmit()
    {
        if (string.IsNullOrWhiteSpace(Content)) return;
        await OnNoteAdded.InvokeAsync(Content);
        Content = string.Empty;
        Message = "Note ajoutée !";
    }
}