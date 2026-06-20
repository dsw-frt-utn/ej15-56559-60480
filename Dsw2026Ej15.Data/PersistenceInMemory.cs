using System.Text.Json;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    private readonly List<Speciality> _specialities = [];
    private readonly List<Doctor> _doctors = [];

    public PersistenceInMemory()
    {
        LoadSpecialities();
    }
    public Speciality? GetSpecialityById(Guid id)
    {
        return _specialities.SingleOrDefault(s => s.Id == id);
    } 
    private async Task LoadSpecialities()
    {
        var json = await File.ReadAllTextAsync("specialities.json");
        var specialities = JsonSerializer.Deserialize<List<Speciality>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (specialities is not null)
            _specialities.AddRange(specialities);
    }

    public IEnumerable<Speciality> GetSpecialities() => _specialities;

    public IEnumerable<Doctor> GetDoctors() => _doctors;

    public Doctor? GetDoctorById(Guid id) =>
        _doctors.FirstOrDefault(d => d.Id == id);

    public void AddDoctor(Doctor doctor) => _doctors.Add(doctor);

    public void UpdateDoctor(Doctor doctor)
    {
        var index = _doctors.FindIndex(d => d.Id == doctor.Id);
        if (index >= 0)
            _doctors[index] = doctor;
    }
}
