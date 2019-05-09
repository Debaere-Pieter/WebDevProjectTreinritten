using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BierSQLIdentity.Services
{
    public class EmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = new MailMessage();

            mail.To.Add(new MailAddress(email));
            mail.From = new MailAddress("pieter.debaere63@gmail.com");
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;

            try
            {
                using (var smtp = new SmtpClient("smtp.gmail.com"))
                {
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("RommelWebDev@gmail.com", "n5XBXSzzp2BZ ");

                    await smtp.SendMailAsync(mail);
                }
            }

            catch (Exception ex)
            {
                //TODO
                throw new InvalidOperationException(ex.Message);
            }
        }

    }
}
