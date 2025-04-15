using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RS1_2024_25.API.Services
{
    public class EmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUsername = "izellapin@gmail.com";
        private readonly string _smtpPassword = "vsho odio tbie lgcz";
        private readonly string _senderEmail = "izellapin@gmail.com";

        public async Task<bool> SendResetEmailAsync(string email, string resetUrl)
        {
            try
            {
                var mail = new MailMessage
                {
                    From = new MailAddress(_senderEmail),
                    Subject = "Password Reset Request",
                    Body = $"<p>Click the link below:</p><p><a href='{resetUrl}'>{resetUrl}</a></p>",
                    IsBodyHtml = true
                };

                mail.To.Add(email);

                using var smtp = new SmtpClient(_smtpServer, _smtpPort)
                {
                    Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                    EnableSsl = true
                };

                await smtp.SendMailAsync(mail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
                return false;
            }
        }
    }
}
