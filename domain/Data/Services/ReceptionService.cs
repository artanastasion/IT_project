using domain.Data.Interfaces;
using domain.Data.Models;


namespace domain.Data.Services;

public class ReceptionService
{
    private readonly IReceptionRepository _repository;

    public ReceptionService(IReceptionRepository repository)
    {
        _repository = repository;

    }
    public Result<List<Reception>> GetReceptionList()
    {
        var result = _repository.GetReceptionList().ToList();
        return result is null ? Result.Fail<List<Reception>>("Appointments not found") : Result.Ok(result);
    }

    public Result<List<Reception>> GetReception(Doctor doctor)
    {
        if (string.IsNullOrEmpty(doctor.Name))
            return Result.Fail<List<Reception>>("Incorrect doctor name");    
        
        var result = _repository.GetReception(doctor).ToList();
        var result1 = result.OrderBy(ob => ob.EndReception).ToList();
        return result1 is null ? Result.Fail<List<Reception>>("Appointments not found") : Result.Ok(result1);
    }

    public Result<Reception> SaveRecord(Reception reception, Schedule schedule)
    {
        var valid = reception.IsValid();
        if (valid.IsFailure)
            return Result.Fail<Reception>("Invalid appointment: " + valid.Error);

        var result1 = schedule.IsValid();
        if (result1.IsFailure)
            return Result.Fail<Reception>("Invalid schedule: " + result1.Error);

        if (schedule.StartDay > reception.StartReception || schedule.EndDay < reception.EndReception)
            return Result.Fail<Reception>("Appointment out of schedule");
        
        var check = _repository.GetReception(reception.Doctor);
        
        if (check.Contains(reception) || schedule.StartDay < reception.StartReception || schedule.EndDay > reception.StartReception)
            return Result.Fail<Reception>("Appointment time not available");

        var result = _repository.SaveRecord(reception, schedule);
        return result is null ? Result.Fail<Reception>("Appointment time not available") : Result.Ok(result);
           
    }
    
    public Result<IEnumerable<Reception>> GetFreeDates(Specialization specialization)
    {
        var result = specialization.IsValid();
        if (result.IsFailure)
            return Result.Fail<IEnumerable<Reception>>("Invalid specialization: " + result.Error);

        return Result.Ok(_repository.GetFreeDates(specialization));
    }
    
    


}   