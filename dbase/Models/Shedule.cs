using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbase.Models;

public class Shedule
{
    [Key]
    public int Id { get; set; }
    
    public int DoctorId { get; set; }

    public Doctor? Doctor { get; set; }

    public DateTime StartDay { get; set; }

    public DateTime EndDay { get; set;}
}