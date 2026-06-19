using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Api.Models;

public record DoctorModel
{
    public record Request(string Name, string LicenseNumber,Guid SpecialityId);

}
