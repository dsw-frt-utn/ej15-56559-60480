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
    public async Task<IActionResult> CreateDoctor([FromBody]DoctorModel.Request request)
    {
        if(string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            throw new ValidationException("Nombre y matricula son requeridos.");
        }
        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality == null)
        {
            throw new ValidationException("La especialidad no existe");
        }
        return Created();
    }
    
}
