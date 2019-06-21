using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Utility.Models;
using System.Threading;
using System.Linq;

namespace LoanMgntAPI.Helper
{
    public class EmailSender
    {
        public static bool SendEmail(long emailTemplateId, Dictionary<string, string> replacements, string toEmail)
        {
            LoanManagementContext _dbContext = new LoanManagementContext();
            try
            {
                EmailTemplate template = _dbContext.EmailTemplate.FirstOrDefault(x => x.EmailTemplateId == emailTemplateId);

                if (template != null)
                {
                    string body = template.Text;
                    replacements.ToList().ForEach(x =>
                    {
                        body = body.Replace(x.Key, Convert.ToString(x.Value));
                    });
                    // Plug in your email service here to send an email.
                    var msg = new MimeMessage();
                    msg.From.Add(new MailboxAddress("LoanAgent Admin", "milind155@gmail.com"));
                    msg.To.Add(new MailboxAddress(toEmail));
                    //msg.Bcc.Add(new MailboxAddress(template.EmailBcc));
                    msg.Subject = template.Title;
                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = body;
                    msg.Body = bodyBuilder.ToMessageBody();


                    using (var smtp = new SmtpClient())
                    {
                        smtp.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                        smtp.Authenticate(credentials: new NetworkCredential("bhanvadiyaankit007@gmail.com", "ankit2635"));
                        smtp.Send(msg, CancellationToken.None);
                        smtp.Disconnect(true, CancellationToken.None);
                    }

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
