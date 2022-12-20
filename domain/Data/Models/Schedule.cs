using System.ComponentModel.DataAnnotations;

namespace domain.Data.Models;

public class Schedule
{
    [Key] public int Id { get; set; }
        
    public virtual Doctor Doctor { get; set; }

    public DateTime StartDay { get; set; }

    public DateTime EndDay { get; set;}
}