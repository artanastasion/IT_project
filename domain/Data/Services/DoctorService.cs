using domain.Data.Interfaces;
using domain.Data.Models;
using Microsoft.AspNetCore.Identity;

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
            return Result.Fail<Doctor>("Incorrect doctor name");

        var doctor = _repository.GetNameDoctor(name);

        return doctor is null ? Result.Fail<Doctor>("Doctor not found") : Result.Ok(doctor);
    }
    
    public Result<Doctor> GetDoctorId(int id)
    {
        if (id < 0)
            return Result.Fail<Doctor>("Incorrect doctor id");
        var doctor = _repository.GetDoctorId(id);

        return doctor is null ? Result.Fail<Doctor>("Doctor not found") : Result.Ok(doctor);
    }
    
    
    
    public Result<Doctor> Create(Doctor doctor)
    {

        if (doctor.IsValid().IsFailure)
            return Result.Fail<Doctor>("Error: " + doctor.IsValid().Error);

        if (_repository.Create(doctor)!.IsValid().Success)
        {
            _repository.Save();
            return Result.Ok(doctor);
        }
        return Result.Fail<Doctor>("Doctor cannot be created");
    }

    public Result<Doctor> Delete(int doctorId)
    {
        var doctorList = _repository.GetDoctorList();
        if (doctorList.Any())
            return Result.Fail<Doctor>("Cannot delete doctor. Doctor has appointments");
        
        var doctor = _repository.GetDoctorId(doctorId);
        if (doctor.IsValid().IsFailure)
            return Result.Fail<Doctor>("Error: " + doctor.IsValid().Error);
        if (_repository.Create(doctor)!.IsValid().Success)
        {
            _repository.Save();
            return Result.Ok(doctor);
        }
        return Result.Fail<Doctor>("Doctor cannot be deleted");
    }

    public Result<List<Doctor>> GetDoctorList()
    {
        var result = _repository.GetDoctorList().ToList();
        return result is null ? Result.Fail<List<Doctor>>("Specialists not found") : Result.Ok(result);
    }

    public Result<List<Doctor>> GetSpecialisationDoctor(Specialization specialization)
        
    {
        if (string.IsNullOrEmpty(specialization.Name))
            return Result.Fail<List<Doctor>>("Incorrect specialisation name");

        var result = _repository.GetSpecialisationDoctor(specialization).ToList();
        return result is null ? Result.Fail<List<Doctor>>("Specialist not found") : Result.Ok(result);
        
    }
    
    
    

}