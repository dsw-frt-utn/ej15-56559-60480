using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    IEnumerable<Speciality> GetSpecialities();
    Speciality? GetSpecialityById(Guid id);

    IEnumerable<Doctor> GetDoctors();
    Doctor? GetDoctorById(Guid id);
    void AddDoctor(Doctor doctor);
    void UpdateDoctor(Doctor doctor);
}
