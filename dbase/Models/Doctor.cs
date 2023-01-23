using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbase.Models;

public class Doctor
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
    public int SpecializationId { get; set; }   
    [ForeignKey("SpecializationId")]
    public Specialization? Specialization { get; set; }
    
    public int SheduleId { get; set; }
    public Shedule? Shedule { get; set; }
}