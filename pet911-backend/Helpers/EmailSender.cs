using System.Net.Mail;
using System.Net;

namespace pet911_backend.Helpers
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("josecnr5@hotmail.com", "Onifantasma453")
            };

            return client.SendMailAsync(
                new MailMessage(from: "josecnr5@hotmail.com",
                                to: email,
                                subject,
                                message
                                ));
        }
    }
}
