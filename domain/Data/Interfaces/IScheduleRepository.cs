using domain.Data.Models;

namespace domain.Data.Interfaces;

public interface IScheduleRepository
{
    IEnumerable<Schedule> GetScheduleList();
    
    Schedule GetScheduleId(int id);
    
    Schedule GetScheduleDoctor(Doctor item, DateTime date);
    
    Schedule CreateScheduleDoctor(Schedule item);
    
    
    
}