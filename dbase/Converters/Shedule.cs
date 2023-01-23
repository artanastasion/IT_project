using SheduleDB = dbase.Models.Shedule;
using SheduleDomain = domain.Data.Models.Schedule;
namespace dbase.converters;

public static class Shedule
{
    public static SheduleDB ToModel(this SheduleDomain model)
    {
        return new SheduleDB
        {
            Id = model.Id,
            DoctorId = model.DoctorId,
            StartDay = model.StartDay,
            EndDay = model.EndDay,
        };
    }
    
    public static SheduleDomain ToDomain(this SheduleDB model)
    {
        return new SheduleDomain
        {
            Id = model.Id,
            DoctorId = model.DoctorId,
            StartDay = model.StartDay,
            EndDay = model.EndDay,
        };
    }
}