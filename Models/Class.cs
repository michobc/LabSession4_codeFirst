using System.ComponentModel.DataAnnotations;
using LabSession4_CodeFirst.ViewModels;

namespace LabSession4_CodeFirst.Models;

public class Class
{
    [Key]
    [Required]
    public int ClassId { get; set; }

    // Foreign keys
    [Required]
    public int CourseId { get; set; }
    
    [Required]
    public int TeacherId { get; set; }
    
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    
    [Required]
    public DateTime StartTime { get; set; }
    
    [Required]
    public DateTime EndTime { get; set; }
}