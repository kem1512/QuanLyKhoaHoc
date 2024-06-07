using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Infrastructure.Data;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Cấu Hình DbContext

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>((sp, options) => options.UseSqlServer(connectionString));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        // Cấu Hình Seed Dữ Liệu
        services.AddScoped<ApplicationDbContextInitialiser>();

        return services;
    }
}
