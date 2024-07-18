using AutoMapper;
using LabSession4_CodeFirst.Models;
using LabSession4_CodeFirst.ViewModels;

namespace LabSession4_CodeFirst.Mappers;

public class UniversityProfile : Profile
{
    public UniversityProfile()
    {
        CreateMap<Student, StudentViewModel>()
            .ForMember(dest => dest.Classes, opt => opt.MapFrom(src => src.Classes.Select(c => c.ClassId)));

        CreateMap<Class, ClassViewModel>()
            .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students.Select(s => s.StudentId)));

        CreateMap<Teacher, TeacherViewModel>()
            .ForMember(dest => dest.Classes, opt => opt.MapFrom(src => src.Classes.Select(c => c.ClassId)));
    }
}