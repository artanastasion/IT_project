using System.ComponentModel.DataAnnotations;

namespace dbase.Models;

public class Reception
{
    [Key] public int Id { get; set; }
        
    public DateTime StartReception { get; set; } = DateTime.MinValue;

    public DateTime EndReception { get; set; } = DateTime.MinValue;
    
    public int UserId { get; set; }
    public int DoctorId { get; set; }
    public List<User> Users { get; set; } = new();
    public List<Doctor> Doctors { get; set; } = new();
}