using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using PaginaInglés.Models;
using System.IO;
//using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PaginaInglés.Servicios
{
    public class ServicioEnvioMail : IEnvioMail
    {
        private readonly ConfiguracionMail _configuracionMail;
        public ServicioEnvioMail(IOptions<ConfiguracionMail> configuracionmail)
        {
            _configuracionMail = configuracionmail.Value;

        }
        public async Task SendEmailAsync(EnvioMail mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(mailRequest.From);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            // esto aplica pra adjuntar archivos
            //if (mailRequest.Attachments != null)
            //{
            //    byte[] fileBytes;
            //    foreach (var file in mailRequest.Attachments)
            //    {
            //        if (file.Length > 0)
            //        {
            //            using (var ms = new MemoryStream())
            //            {
            //                file.CopyTo(ms);
            //                fileBytes = ms.ToArray();
            //            }
            //            builder.Attachments.Add(file.FileName, fileBytes, MimeKit.ContentType.Parse(file.ContentType));
            //        }
            //    }
            //}
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_configuracionMail.Host, _configuracionMail.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuracionMail.Mail, _configuracionMail.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
