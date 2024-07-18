using AutoMapper;
using LabSession4_CodeFirst.Models;
using LabSession4_CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LabSession4_CodeFirst.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly UniversityContext _context;
        private readonly IMapper _mapper;

        public UniversityController(UniversityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GETTERS USING VIEWMODEL
        [HttpGet("students/{id}")]
        public async Task<ActionResult<StudentViewModel>> GetStudent(int id)
        {
            var student = await _context.Students
                .Include(s => s.Classes)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            var studentViewModel = _mapper.Map<StudentViewModel>(student);
            return studentViewModel;
        }

        [HttpGet("teachers/{id}")]
        public async Task<ActionResult<TeacherViewModel>> GetTeacher(int id)
        {
            var teacher = await _context.Teachers
                .Include(t => t.Classes)
                .FirstOrDefaultAsync(t => t.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }
            var teacherViewModel = _mapper.Map<TeacherViewModel>(teacher);
            return teacherViewModel;
        }

        [HttpGet("classes/{id}")]
        public async Task<ActionResult<ClassViewModel>> GetClass(int id)
        {
            // @class => treated as an identifier
            var @class = await _context.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.ClassId == id);

            if (@class == null)
            {
                return NotFound();
            }
            var classViewModel = _mapper.Map<ClassViewModel>(@class);
            return classViewModel;
        }

        [HttpPost("students")]
        public async Task<ActionResult<StudentViewModel>> AddStudent([FromForm] Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            var studentViewModel = _mapper.Map<StudentViewModel>(student);
            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, studentViewModel);
        }

        [HttpPost("teachers")]
        public async Task<ActionResult<TeacherViewModel>> AddTeacher([FromForm] Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            var teacherViewModel = _mapper.Map<TeacherViewModel>(teacher);
            return CreatedAtAction(nameof(GetTeacher), new { id = teacher.TeacherId }, teacherViewModel);
            // returns a 201 created response with the uri of the new resource
        }

        [HttpPost("classes")]
        public async Task<ActionResult<ClassViewModel>> AddClass([FromForm] Class @class)
        {
            _context.Classes.Add(@class);
            await _context.SaveChangesAsync();
            var classViewModel = _mapper.Map<ClassViewModel>(@class);
            return CreatedAtAction(nameof(GetClass), new { id = @class.ClassId }, classViewModel);
        }

        [HttpPost("classes/{classId}/enroll/{studentId}")]
        public async Task<ActionResult> EnrollInClass(int classId, int studentId)
        {
            var @class = await _context.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.ClassId == classId);
            if (@class == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
            {
                return NotFound();
            }
            if (@class.Students.Any(s => s.StudentId == studentId))
            {
                return Conflict("Student is already enrolled in the class.");
            }

            @class.Students.Add(student);
            student.Classes.Add(@class);
            await _context.SaveChangesAsync();
            return Ok(new { student.StudentId, student.Name });
        }

        [HttpDelete("classes/{classId}/remove/{studentId}")]
        public async Task<ActionResult> RemoveFromClass(int classId, int studentId)
        {
            var @class = await _context.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.ClassId == classId);

            if (@class == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
            {
                return NotFound();
            }

            if (@class.Students.All(s => s.StudentId != studentId))
            {
                return NotFound("Student is not enrolled in the class.");
            }

            @class.Students.Remove(student); // Remove student from class
            student.Classes.Remove(@class); // Remove class from student

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}