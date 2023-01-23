using System.ComponentModel.DataAnnotations;

namespace dbase.Models;

public class Specialization
{
    [Key]
    public int Id { get; set; }
    [MaxLength(60)]   
    public string Name { get; set; }
    public List<Doctor> Doctors { get; set; } = new();
}