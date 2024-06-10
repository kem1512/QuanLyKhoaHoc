using Microsoft.Extensions.Options;
using QuanLyKhoaHoc.Application.Common.Models;
using System.Net;
using System.Net.Mail;

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

        // Cấu Hình Mail
        var emailSettings = configuration.GetSection("EmailSettings");

        services.AddScoped(provider =>
        {
            var emailSettings = provider.GetRequiredService<IOptions<EmailSettings>>().Value;

            var client = new SmtpClient(emailSettings.Host, emailSettings.Port)
            {
                Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password),
                EnableSsl = emailSettings.EnableSsl
            };

            return client;
        });

        return services;
    }
}
