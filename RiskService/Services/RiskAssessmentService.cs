namespace RiskService.Services;

public class RiskAssessmentService
{
    private static readonly string[] Triggers =
    [
        "Hémoglobine A1C", "Microalbumine", "Taille", "Poids",
        "Fumeur", "Fumeuse", "Anormal", "Cholestérol",
        "Vertiges", "Rechute", "Réaction", "Anticorps"
    ];

    public string Assess(int age, string gender, int triggerCount)
    {
        bool isMale = gender.Equals("M", StringComparison.OrdinalIgnoreCase)
                   || gender.Equals("Male", StringComparison.OrdinalIgnoreCase)
                   || gender.Equals("Homme", StringComparison.OrdinalIgnoreCase);
        bool isUnder30 = age < 30;

        if (triggerCount == 0)
            return "None";

        // Early onset
        if (isUnder30 && isMale && triggerCount >= 5) return "EarlyOnset";
        if (isUnder30 && !isMale && triggerCount >= 7) return "EarlyOnset";
        if (!isUnder30 && triggerCount >= 8) return "EarlyOnset";

        // In Danger
        if (isUnder30 && isMale && triggerCount >= 3) return "InDanger";
        if (isUnder30 && !isMale && triggerCount >= 4) return "InDanger";
        if (!isUnder30 && triggerCount >= 6) return "InDanger";

        // Borderline
        if (!isUnder30 && triggerCount >= 2) return "Borderline";

        return "None";
    }

    public int CountTriggers(IEnumerable<string> noteContents)
    {
        var allText = string.Join(" ", noteContents);
        return Triggers.Count(t =>
            allText.Contains(t, StringComparison.OrdinalIgnoreCase));
    }
}