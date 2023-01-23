namespace domain.Data.Models
{
    public class Users
    {
        public int Id { get; set; }
        
        public string Login { get; set; }
        public string Password { get; set; }
            
        public virtual Role Role { get; set; }
        public virtual int RoleId { get; set; }
            
        public string PhoneNumber { get; set; }
    }
}

