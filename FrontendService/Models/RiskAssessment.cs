namespace FrontendService.Models;

public class RiskAssessment
{
    public int PatientId { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; } = string.Empty;
    public int TriggerCount { get; set; }
    public string Assessment { get; set; } = string.Empty;
}