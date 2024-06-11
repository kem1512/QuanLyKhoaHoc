using Microsoft.Extensions.Options;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Models;
using System.Net;
using System.Net.Mail;
using QuanLyKhoaHoc.Application.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Cấu Hình DbContext
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        // Cấu Hình Seed Dữ Liệu
        services.AddScoped<ApplicationDbContextInitialiser>();

        // Cấu Hình Mail
        var emailSettings = configuration.GetSection("EmailSettings");

        services.AddTransient(provider =>
        {
            var client = new SmtpClient(emailSettings["Host"], int.Parse(emailSettings["Port"] ?? "25"))
            {
                Credentials = new NetworkCredential(emailSettings["Username"], emailSettings["Password"]),
                EnableSsl = bool.Parse(emailSettings["EnableSsl"] ?? "false")
            };
            return client;
        });

        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}
