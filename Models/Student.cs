using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using LabSession4_CodeFirst.ViewModels;

namespace LabSession4_CodeFirst.Models;

public class Student
{
    [Key]
    [Required]
    public int StudentId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public DateTime DateOfBirth { get; set; }
    
    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}