using FrontendService.Models;
using Microsoft.AspNetCore.Components;

namespace FrontendService.Components.Shared;

public partial class PatientForm
{
    [Parameter, EditorRequired]
    public Patient Patient { get; set; } = new();
}