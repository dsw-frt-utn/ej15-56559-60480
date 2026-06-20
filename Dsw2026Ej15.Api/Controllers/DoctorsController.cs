using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Excepciones;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Dsw2026Ej15.Api.Models.DoctorModel;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("/api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost]
    public IActionResult Create([FromBody] DoctorModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
            throw new ValidationException("Nombre y matrícula son requeridos.");

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality is null)
            throw new ValidationException("La especialidad no existe.");

        var doctor = new Doctor(request.Name, request.LicenseNumber, true, speciality);
        _persistence.AddDoctor(doctor);

        return Created();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var doctors = _persistence.GetDoctors()
            .Where(d => d.IsActive)
            .Select(d => new DoctorModel.Response(d.Name, d.LicenseNumber, d.Speciality!.Name));

        return Ok(doctors);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);
        if (doctor is null || !doctor.IsActive)
            return NotFound();

        return Ok(new DoctorModel.Response(doctor.Name, doctor.LicenseNumber, doctor.Speciality!.Name));
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);
        if (doctor is null || !doctor.IsActive)
            return NotFound();

        doctor.IsActive = false;
        _persistence.UpdateDoctor(doctor);

        return NoContent();
    }

}
