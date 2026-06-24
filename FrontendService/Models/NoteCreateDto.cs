namespace FrontendService.Models;

public class NoteCreateDto
{
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}