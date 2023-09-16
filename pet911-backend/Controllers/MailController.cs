using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace pet911_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        [HttpPost]
        public ActionResult SendMail()
        {
            Send();
            return Ok("Se envio un mensaje");
        }
        private void Send()
        {
            // Configuración del servidor SMTP
            string smtpServer = "smtp.mailgun.org";
            int smtpPort = 587;
            string smtpUsername = "postmaster@sandboxefb521b462f64ec6a79d64fb7522e1e6.mailgun.org";
            string smtpPassword = "48abaedb440bbf2f26557d771e45217e-6d1c649a-1788a241";

            // Configuración del correo electrónico
            string from = "giantex5@gmail.com";
            string to = "josecm0998@gmail.com";
            string subject = "Prueba de envío de correo";
            string body = "Hola, esto es un correo de prueba enviado desde .NET.";

            // Crear el mensaje de correo electrónico
            MailMessage message = new MailMessage(from, to, subject, body);

            // Crear el cliente SMTP y enviar el mensaje
            SmtpClient client = new SmtpClient(smtpServer, smtpPort);
            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            client.EnableSsl = true;

            try
            {
                client.Send(message);
                Console.WriteLine("Correo enviado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }
        }
    }
   
}
