using AutoMapper;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Application
{
    public class ApplicationMapping : Profile
    {
        public ApplicationMapping()
        {
            CreateMap<Subject, SubjectMapping>().ReverseMap();
            CreateMap<Subject, SubjectCreate>().ReverseMap();
            CreateMap<Subject, SubjectUpdate>().ReverseMap();

            CreateMap<Course, CourseMapping>().ReverseMap();
            CreateMap<Course, CourseCreate>().ReverseMap();
            CreateMap<Course, CourseUpdate>().ReverseMap();

            CreateMap<Blog, BlogMapping>().ReverseMap();
            CreateMap<Blog, BlogCreate>().ReverseMap();
            CreateMap<Blog, BlogUpdate>().ReverseMap();

            CreateMap<CourseSubject, CourseSubjectMapping>().ReverseMap();
        }
    }
}
