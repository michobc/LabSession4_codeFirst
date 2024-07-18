namespace LabSession4_CodeFirst.ViewModels;

public class TeacherViewModel
{
    public int TeacherId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<int> Classes { get; set; }
}