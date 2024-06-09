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

            CreateMap<Province, ProvinceMapping>().ReverseMap();
            CreateMap<Province, ProvinceCreate>().ReverseMap();
            CreateMap<Province, ProvinceUpdate>().ReverseMap();

            CreateMap<District, DistrictMapping>().ReverseMap();
            CreateMap<District, DistrictCreate>().ReverseMap();
            CreateMap<District, DistrictUpdate>().ReverseMap();

            CreateMap<Ward, WardMapping>().ReverseMap();
            CreateMap<Ward, WardCreate>().ReverseMap();
            CreateMap<Ward, WardUpdate>().ReverseMap();

            CreateMap<User, UserMapping>().ReverseMap();
            CreateMap<User, UserCreate>().ReverseMap();
            CreateMap<User, UserUpdate>().ReverseMap();

            CreateMap<Role, RoleMapping>().ReverseMap();
            CreateMap<Role, RoleCreate>().ReverseMap();
            CreateMap<Role, RoleUpdate>().ReverseMap();

            CreateMap<Certificate, CertificateMapping>().ReverseMap();
            CreateMap<Certificate, CertificateCreate>().ReverseMap();
            CreateMap<Certificate, CertificateUpdate>().ReverseMap();

            CreateMap<CertificateType, CertificateTypeMapping>().ReverseMap();
            CreateMap<CertificateType, CertificateTypeCreate>().ReverseMap();
            CreateMap<CertificateType, CertificateTypeUpdate>().ReverseMap();

            CreateMap<CourseSubject, CourseSubjectMapping>().ReverseMap();
        }
    }
}
