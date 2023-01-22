using System.Globalization;

namespace domain.Data.Services;
using Interfaces;
using Models;


public class ScheduleService
{
    private readonly IScheduleRepository _repository;
    
    public ScheduleService(IScheduleRepository repository)
    {
        _repository = repository;
    }
    
    public Result<List<Schedule>> GetScheduleList()
    {
        var result = _repository.GetScheduleList().ToList();
        return result is null ? Result.Fail<List<Schedule>>("Shedules not found") : Result.Ok(result);
    }

    public Result<Schedule> GetScheduleDoctor(Doctor doctor, DateTime date)
    {
        if (doctor.IsValid().IsFailure)
            return Result.Fail<Schedule>("Error: " + doctor.IsValid().Error);

        var doctorSchedule = _repository.GetScheduleDoctor(doctor, date);
        return doctorSchedule is null ? Result.Fail<Schedule>("Shedule not found") : Result.Ok(doctorSchedule);
        
    }
    
    public Result<Schedule> CreateScheduleDoctor(Schedule schedule)
    {
        if (schedule.IsValid().IsFailure)
            return Result.Fail<Schedule>("Error: " + schedule.IsValid().Error);
        
        String endString = schedule.EndDay.ToString(); 
        String startString = schedule.StartDay.ToString();
        DateTime end = schedule.EndDay;
        DateTime start = schedule.StartDay;
        
        
        
        if (string.IsNullOrEmpty(endString) || !DateTime.TryParseExact(endString, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end))
        {
            return Result.Fail<Schedule>("Incorrect date");
        }
        
        if (string.IsNullOrEmpty(startString) || !DateTime.TryParseExact(startString, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
        {
            return Result.Fail<Schedule>("Incorrect date");
        }
        

        var result = _repository.CreateScheduleDoctor(schedule);
        return result is null ? Result.Fail<Schedule>("Shedule not found") : Result.Ok(result);
        
    }
}