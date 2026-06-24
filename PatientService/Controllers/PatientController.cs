using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientService.Data;
using PatientService.Models;

namespace PatientService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController(PatientDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await db.Patients.ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var patient = await db.Patients.FindAsync(id);
        return patient is null ? NotFound() : Ok(patient);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Patient patient)
    {
        db.Patients.Add(patient);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Patient patient)
    {
        var existing = await db.Patients.FindAsync(id);
        if (existing is null) return NotFound();

        existing.FirstName = patient.FirstName;
        existing.LastName = patient.LastName;
        existing.DateOfBirth = patient.DateOfBirth;
        existing.Gender = patient.Gender;
        existing.Address = patient.Address;
        existing.PhoneNumber = patient.PhoneNumber;

        await db.SaveChangesAsync();
        return Ok(existing);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var patient = await db.Patients.FindAsync(id);
        if (patient is null) return NotFound();

        db.Patients.Remove(patient);
        await db.SaveChangesAsync();
        return NoContent();
    }
}