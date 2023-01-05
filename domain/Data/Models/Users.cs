using System.ComponentModel.DataAnnotations;

namespace domain.Data.Models
{
    public class Users
    {
        [Key] public int Id { get; set; }
        
        [MaxLength(255)] public string Login { get; set; }
            
        public virtual Role Role { get; set; }
            
        public string PhoneNumber { get; set; }
    }
}

