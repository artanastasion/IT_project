using domain.Data.Models;

namespace domain.Data.Interfaces;

public interface IDoctorRepository
{
    IEnumerable<Doctor> GetDoctorList();
    Doctor GetNameDoctor(string name);

    Doctor GetSpecialisationDoctor(Specialization item);
    Doctor GetDoctorId(int id);
    Doctor Create(Doctor item);
    Doctor Delete(int id);    
    Doctor Save();
    Doctor Update(Doctor item);

}