using QuanLyKhoaHoc.Application;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Services;
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

        services.AddScoped<ApplicationServiceBase<WardMapping, WardQuery, WardCreate, WardUpdate>, WardService>();

        services.AddTransient<IJwtService, JwtService>();

        return services;
    }
}