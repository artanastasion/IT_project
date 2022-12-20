using System.ComponentModel.DataAnnotations;

namespace domain.Models;

public class Doctor
{
    [Key] public int Id { get; set; }

    [MaxLength(255)] public string Name { get; set; }

    public virtual Specialization Specialization { get; set; }
}