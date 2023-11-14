using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
namespace GoMyShops.Commons
{
    public class EmailService : IEmailSender//IIdentityMessageService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            await configSMTPasync(email, subject, message);
            //return Task.FromResult(0);
        }

        public async Task configSMTPasync(string email, string subject, string message)
        {
            //SmtpClient smtp = new SmtpClient();
            //Todo Harris (Test) Modify Core

            var Message = new MailMessage("harrisyer@gmail.com", email);
            Message.Subject = subject;
            Message.Body = message;
            Message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;//25, 465 & 587
                smtp.EnableSsl = true;
                smtp.ServicePoint.MaxIdleTime = 2;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                smtp.Credentials = new NetworkCredential("harrisyer@gmail.com", "sswx zaqi pksc zqmy");
                // await smtp.ConnectAsync("smtp.relay.uri", 25, SecureSocketOptions.None).ConfigureAwait(false);
                await smtp.SendMailAsync(Message).ConfigureAwait(false);
                //await smtp.DisconnectAsync(true).ConfigureAwait(false);
            }


            //Task.Factory.StartNew(() => smtp.Send(Message));
        }

    }
}
