namespace QuanLyKhoaHoc.Application.Services
{
    internal class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var mailMessage = new MailMessage("noreply@yourdomain.com", toEmail, subject, body);
            mailMessage.IsBodyHtml = true;
            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
