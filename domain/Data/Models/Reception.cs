using System.ComponentModel.DataAnnotations;
using System.Data;

namespace domain.Data.Models;

public class Reception
{
    [Key] public int Id { get; set; }
        
    public DateTime StartReception { get; set; }

    public DateTime EndReception { get; set; }

    public virtual Doctor Doctor { get; set; }
    public int DoctorId { get; set; }
    public int UserId { get; set; }


    public virtual Users User { get; set; }

    public Reception(int id, DateTime startReception, DateTime endReception, int doctor, int user)
    {
        Id = id;
        StartReception = startReception;
        EndReception = endReception;
        Doctor.Id = doctor;
        User.Id = user;

    }
    
    public Reception(DateTime startReception, DateTime endReception, int doctor,int user) 
        : this(0, startReception, endReception, doctor, user) { }
    
    public Reception() : this(0, DateTime.MinValue, DateTime.MinValue, new Doctor().Id, new Users().Id) { }
    
    public Result IsValid()
    {
        if (Id < 0)
            return Result.Fail("Incorrect ID");
        if (User.Id < 0)
            return Result.Fail("Incorrect patient ID");
        if (Doctor.Id < 0)
            return Result.Fail("Incorrect doctor ID");
        if (StartReception > EndReception)
            return Result.Fail("Incorrect");
        return Result.Ok();
    }
}