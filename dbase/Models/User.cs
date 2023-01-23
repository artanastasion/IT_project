using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace dbase.Models;
public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }
    public string? Phonenumber { get; set; }
    public string? Password { get; set; }
    public int Age { get; set; }
    public int RoleId { get; set; }   
    [ForeignKey("RoleId")]
    public Role? Role { get; set; } 
    
    public List<Reception> Receptions { get; set; } = new();
    
}
