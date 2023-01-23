using dbase.Models;
using Microsoft.EntityFrameworkCore;

namespace dbase;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property("Id").HasField("id");
        modelBuilder.Entity<User>().Property("name");
        modelBuilder.Entity<User>().Property("Role").HasField("role");
        modelBuilder.Entity<User>().Property("Age").HasField("age");
        modelBuilder.Entity<User>().HasCheckConstraint("Age", "Age > 0 AND Age < 120");
        
        modelBuilder.Entity<Reception>()
            .HasMany(c => c.Users)
            .WithMany(s => s.Receptions)
            .UsingEntity(j => j.ToTable("Receptions"));
        
        
    }
}