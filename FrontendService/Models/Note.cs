using System.Text.Json.Serialization;
namespace FrontendService.Models;

public class Note
{
    [JsonPropertyName("id")]
    public MongoId? Id { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class MongoId
{
    [JsonPropertyName("$oid")]
    public string Oid { get; set; } = string.Empty;
}