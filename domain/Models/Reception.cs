using System.ComponentModel.DataAnnotations;

namespace domain.Models;

public class Reception
{
    [Key] public int Id { get; set; }
        
    public DateTime StartReception { get; set; }

    public DateTime EndReception { get; set; }

    public virtual Doctor Doctor { get; set; }

    public virtual Users User { get; set; }
}