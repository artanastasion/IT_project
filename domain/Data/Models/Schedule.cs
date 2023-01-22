using System.ComponentModel.DataAnnotations;
using System.Data;

namespace domain.Data.Models;

public class Schedule
{
    public int Id { get; set; }
        
    public int DoctorId { get; set; }

    public DateTime StartDay { get; set; }

    public DateTime EndDay { get; set;}
    
    public Schedule(int id, int doctorId, DateTime startDay, DateTime endDay)
    {
        Id = id;
        DoctorId = doctorId;
        StartDay = startDay;
        EndDay = endDay;
    }
    
    public Schedule() : this(0, 0, DateTime.MinValue, DateTime.MaxValue) { }

    public Result IsValid()
    {
        if (DoctorId < 0)
            return Result.Fail("Incorrect id");
        if (DoctorId < 0)
            return Result.Fail("Incorrect doctor id");
        if (StartDay > EndDay)
            return Result.Fail("Incorrect time");
        return Result.Ok();


    }
}
