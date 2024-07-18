using System.ComponentModel.DataAnnotations;
using LabSession4_CodeFirst.ViewModels;

namespace LabSession4_CodeFirst.Models;

public class Teacher
{
    [Key]
    [Required]
    public int TeacherId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public DateTime DateOfBirth { get; set; }
    
    public virtual ICollection<Class> Classes { get; set; }  = new List<Class>();
}