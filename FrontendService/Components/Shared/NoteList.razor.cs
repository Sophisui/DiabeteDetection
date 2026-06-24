using FrontendService.Models;
using Microsoft.AspNetCore.Components;

namespace FrontendService.Components.Shared;

public partial class NoteList
{
    [Parameter, EditorRequired]
    public List<Note>? Notes { get; set; }
}