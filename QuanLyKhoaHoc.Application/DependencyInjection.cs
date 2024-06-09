using QuanLyKhoaHoc.Application;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Services;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<ApplicationServiceBase<SubjectMapping, SubjectQuery, SubjectCreate, SubjectUpdate>, SubjectService>();

        services.AddScoped<ApplicationServiceBase<CourseMapping, CourseQuery, CourseCreate, CourseUpdate>, CourseService>();

        services.AddScoped<ApplicationServiceBase<BlogMapping, BlogQuery, BlogCreate, BlogUpdate>, BlogService>();

        services.AddScoped<ApplicationServiceBase<ProvinceMapping, ProvinceQuery, ProvinceCreate, ProvinceUpdate>, ProvinceService>();

        services.AddScoped<ApplicationServiceBase<DistrictMapping, DistrictQuery, DistrictCreate, DistrictUpdate>, DistrictService>();

        services.AddScoped<ApplicationServiceBase<UserMapping, UserQuery, UserCreate, UserUpdate>, UserService>();

        services.AddScoped<ApplicationServiceBase<WardMapping, WardQuery, WardCreate, WardUpdate>, WardService>();

        services.AddScoped<ApplicationServiceBase<RoleMapping, RoleQuery, RoleCreate, RoleUpdate>, RoleService>();

        services.AddScoped<ApplicationServiceBase<CertificateMapping, CertificateQuery, CertificateCreate, CertificateUpdate>, CertificateService>();

        services.AddScoped<ApplicationServiceBase<CertificateTypeMapping, CertificateTypeQuery, CertificateTypeCreate, CertificateTypeUpdate>, CertificateTypeService>();

        services.AddTransient(provider =>
        {
            var client = new SmtpClient("smtp.example.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("username", "password"),
                EnableSsl = true,
            };
            return client;
        });

        services.AddScoped<IEmailService, EmailService>();

        services.AddTransient<IAuthService, AuthService>();

        return services;
    }
}