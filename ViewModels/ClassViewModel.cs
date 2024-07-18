namespace LabSession4_CodeFirst.ViewModels;

public class ClassViewModel
{
    public int ClassId { get; set; }
    public int CourseId { get; set; }
    public int TeacherId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ICollection<int> Students { get; set; }
}