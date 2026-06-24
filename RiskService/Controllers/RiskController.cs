using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiskService.Models;
using RiskService.Services;
using System.Net.Http.Headers;

namespace RiskService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RiskController(IHttpClientFactory httpClientFactory, RiskAssessmentService riskService) : ControllerBase
{
    [HttpGet("{patientId:int}")]
    public async Task<IActionResult> GetRisk(int patientId)
    {
        var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        var client = httpClientFactory.CreateClient("gateway");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var patientResponse = await client.GetAsync($"api/patient/{patientId}");
        if (!patientResponse.IsSuccessStatusCode) return NotFound("Patient introuvable.");

        var patient = await patientResponse.Content.ReadFromJsonAsync<PatientDto>();
        if (patient is null) return NotFound();

        var notesResponse = await client.GetAsync($"api/note/patient/{patientId}");
        var notes = notesResponse.IsSuccessStatusCode
            ? await notesResponse.Content.ReadFromJsonAsync<List<NoteDto>>() ?? []
            : [];

        var age = CalculateAge(patient.DateOfBirth);
        var triggerCount = riskService.CountTriggers(notes.Select(n => n.Content));
        var result = riskService.Assess(age, patient.Gender, triggerCount);

        return Ok(new
        {
            patientId,
            age,
            gender = patient.Gender,
            triggerCount,
            assessment = result
        });
    }

    private static int CalculateAge(DateOnly dob)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - dob.Year;
        if (dob > today.AddYears(-age)) age--;
        return age;
    }
}