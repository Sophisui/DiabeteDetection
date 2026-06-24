using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NoteService.Models;

public class Note
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("patientId")]
    public int PatientId { get; set; }

    [BsonElement("patientName")]
    public string PatientName { get; set; } = string.Empty;

    [BsonElement("content")]
    public string Content { get; set; } = string.Empty;

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}