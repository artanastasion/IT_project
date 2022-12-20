
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace domain.Models
{
    [Table(nameof(Users))]
    public class Users
    {
        [Key] public int Id { get; set; }
        
        [MaxLength(255)] public string Login { get; set; }
            
        public virtual Role Role { get; set; }
            
        public string PhoneNumber { get; set; }
    }
}

