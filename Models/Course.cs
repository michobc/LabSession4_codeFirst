using System.ComponentModel.DataAnnotations;
using LabSession4_CodeFirst.ViewModels;

namespace LabSession4_CodeFirst.Models;

public class Course
{
    [Key]
    [Required]
    public int CourseId { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Language { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}