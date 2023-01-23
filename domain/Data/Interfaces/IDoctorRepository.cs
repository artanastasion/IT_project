using domain.Data.Models;

namespace domain.Data.Interfaces;

public interface IDoctorRepository : IRepository<Doctor>
{
    IEnumerable<Doctor> GetDoctorList();
    Doctor GetNameDoctor(string name);
    IEnumerable<Doctor> GetSpecialisationDoctor(Specialization item);
    Doctor GetDoctorId(int id);

}