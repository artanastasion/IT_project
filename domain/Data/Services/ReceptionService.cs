using domain.Data.Interfaces;
using domain.Data.Models;


namespace domain.Data.Services;

public class ReceptionService
{
    private readonly IReceptionRepository _repository;
    private static readonly Dictionary<int, Mutex> _mutexDictionary = new Dictionary<int, Mutex>();

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
        
        var appointments = _repository.GetReception(reception.DoctorId).ToList();
        appointments.Sort((a, b) => { return (a.StartReception < b.StartReception) ? -1 : 1; });
        var index = appointments.FindLastIndex(a => a.EndReception <= reception.StartReception);
        if (appointments.Count > index + 1)
        {
            if (appointments[index + 1].StartReception < reception.EndReception)
                return Result.Fail<Reception>("Appointment time already taken");
        }

        if (!_mutexDictionary.ContainsKey(reception.DoctorId))
            _mutexDictionary.Add(reception.DoctorId, new Mutex());
        _mutexDictionary.First(d => d.Key == reception.DoctorId).Value.WaitOne();
        
        if (_repository.Create(reception).IsValid().Success)
        {
            _repository.Save();
            _mutexDictionary.First(d => d.Key == reception.DoctorId).Value.ReleaseMutex();
            return Result.Ok(reception);
        }
        return Result.Fail<Reception>("Unable to save appointment");
        
    }
    
    public Result<IEnumerable<Reception>> GetFreeDates(Specialization specialization)
    {
        var result = specialization.IsValid();
        if (result.IsFailure)
            return Result.Fail<IEnumerable<Reception>>("Invalid specialization: " + result.Error);

        return Result.Ok(_repository.GetFreeDates(specialization));
    }
    
    


}   