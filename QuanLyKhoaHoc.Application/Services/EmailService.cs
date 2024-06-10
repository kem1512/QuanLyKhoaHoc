﻿namespace QuanLyKhoaHoc.Application.Services
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
            if (_smtpClient == null)
            {
                return;
            }

            var mailMessage = new MailMessage
            {
                From = new MailAddress("Hệ Thống"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
