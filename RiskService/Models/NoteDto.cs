namespace RiskService.Models;

public class NoteDto
{
    public int PatientId { get; set; }
    public string Content { get; set; } = string.Empty;
}