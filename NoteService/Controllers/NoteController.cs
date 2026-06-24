using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteService.Data;
using NoteService.Models;

namespace NoteService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController(NoteDbContext db) : ControllerBase
{
    [HttpGet("patient/{patientId:int}")]
    public async Task<IActionResult> GetByPatient(int patientId) =>
        Ok(await db.Notes.Where(n => n.PatientId == patientId).ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Note note)
    {
        note.CreatedAt = DateTime.UtcNow;
        db.Notes.Add(note);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetByPatient), new { patientId = note.PatientId }, note);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Note note)
    {
        var existing = await db.Notes.FindAsync(MongoDB.Bson.ObjectId.Parse(id));
        if (existing is null) return NotFound();

        existing.Content = note.Content;
        await db.SaveChangesAsync();
        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var note = await db.Notes.FindAsync(MongoDB.Bson.ObjectId.Parse(id));
        if (note is null) return NotFound();

        db.Notes.Remove(note);
        await db.SaveChangesAsync();
        return NoContent();
    }
}