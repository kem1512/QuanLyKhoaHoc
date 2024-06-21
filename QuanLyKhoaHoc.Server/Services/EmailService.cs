
using System.Net.Mail;

namespace QuanLyKhoaHoc.Server.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _config;

        public EmailService(SmtpClient smtpClient, IConfiguration config)
        {
            _smtpClient = smtpClient;
            _config = config;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            if (_smtpClient == null)
            {
                return;
            }

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config["EmailSettings:Username"] ?? "admin@khoahocviet.nguyenviethaidang.id.vn"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
