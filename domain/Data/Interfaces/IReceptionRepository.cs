using domain.Data.Models;

namespace domain.Data.Interfaces;

public interface IReceptionRepository
{
    IEnumerable<Reception> GetReceptionList();
    
    IEnumerable<Reception> GetReception(Doctor item);
    Reception SaveRecord(Reception reception, Schedule schedule);
    
    IEnumerable<Reception> GetFreeDates(Specialization item);

}