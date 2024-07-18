namespace LabSession4_CodeFirst.ViewModels;

public class StudentViewModel
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<int> Classes { get; set; }
}