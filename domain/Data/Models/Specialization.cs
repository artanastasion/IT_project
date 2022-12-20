using System.ComponentModel.DataAnnotations;

namespace domain.Data.Models;

public class Specialization
{
    [Key] public int Id { get; set; }
        
    [MaxLength(255)] public string Name { get; set; }
}