using Microsoft.EntityFrameworkCore;

namespace LabSession4_CodeFirst.Models;

public class UniversityContext : DbContext
{
    public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) { }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Class> Classes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1. One-to-Many: Course to Classes
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Classes)
            .WithOne()
            .HasForeignKey(cl => cl.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    
        // 2. One-to-Many: Teacher to Classes
        modelBuilder.Entity<Teacher>()
            .HasMany(t => t.Classes)
            .WithOne()
            .HasForeignKey(cl => cl.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);
    
        // 3. Many-to-Many: Student to Classes
        modelBuilder.Entity<Student>()
            .HasMany(s => s.Classes)
            .WithMany(cl => cl.Students)
            .UsingEntity(j => j.ToTable("StudentClassEnrollments"));
    
        base.OnModelCreating(modelBuilder);
    }
}


// create migration:
// dotnet ef migrations add InitialCreate
// apply migration:
// dotnet ef database update