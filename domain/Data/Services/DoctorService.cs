using domain.Data.Interfaces;
using domain.Data.Models;

namespace domain.Data.Services;

public class DoctorService
{
    private readonly IDoctorRepository _repository;

    public DoctorService(IDoctorRepository repository)
    {
        _repository = repository;
    }
        
    public Result<Doctor> GetNameDoctor(string name)
    {
        if (string.IsNullOrEmpty(name))
            return Result.Fail<Doctor>("Укажите ФИО врача");

        var doctor = _repository.GetNameDoctor(name);

        return doctor is null ? Result.Fail<Doctor>("Врач не найден") : Result.Ok(doctor);
    }
    
    public Result<Doctor> GetDoctorId(int id)
    {
        var doctor = _repository.GetDoctorId(id);

        return doctor is null ? Result.Fail<Doctor>("Врач не найден") : Result.Ok(doctor);
    }
    
    
    
    public Result<Doctor> Create(Doctor doctor)
    {
        if (string.IsNullOrEmpty(doctor.Name))
                return Result.Fail<Doctor>("Укажите ФИО врача");
            
        if (string.IsNullOrEmpty(doctor.Specialization.Name))
            return Result.Fail<Doctor>("Укажите специализацию врача");

        var doctorCreate = _repository.Create(doctor);
        return doctorCreate is null ? Result.Fail<Doctor>("Врач не может быть создан") : Result.Ok(doctorCreate);
            
    }

    public Result<Doctor> Delete(int doctorId)
    {
        var doctorDelete = _repository.Delete(doctorId);
        return doctorDelete is null ? Result.Fail<Doctor>("Врач не найден") : Result.Ok(doctorDelete);

    }

    public Result<List<Doctor>> GetDoctorList()
    {
        var result = _repository.GetDoctorList().ToList();
        return result is null ? Result.Fail<List<Doctor>>("Специалисты не найдены") : Result.Ok(result);
    }

    public Result<Doctor> GetSpecialisationDoctor(Specialization specialization)
    {
        if (string.IsNullOrEmpty(specialization.Name))
            return Result.Fail<Doctor>("Укажите специализацию врача");

        var result = _repository.GetSpecialisationDoctor(specialization);
        return result is null ? Result.Fail<Doctor>("Специалист не найден") : Result.Ok(result);
        
    }

}