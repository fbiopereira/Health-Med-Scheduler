using HealthMedScheduler.Application.Interfaces.Email;
using HealthMedScheduler.Application.Models;
using HealthMedScheduler.Application.ViewModel;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace HealthMedScheduler.Infrastructure.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(EmailViewModel emailViewModel)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress(_emailSettings.Mail, _emailSettings.DisplayName);
            message.To.Add(new MailAddress(emailViewModel.ToEmail));
            message.Subject = emailViewModel.Subject;

            message.IsBodyHtml = false;
            message.Body = emailViewModel.Body;
            smtp.Port = _emailSettings.Port;
            smtp.Host = _emailSettings.Host;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_emailSettings.Mail, _emailSettings.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            await smtp.SendMailAsync(message);
        }
    }
}
