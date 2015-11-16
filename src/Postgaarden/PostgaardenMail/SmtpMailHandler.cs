using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using PostgaardenMail.Dummy;

namespace PostgaardenMail
{
    /*
        Developed by Martin Hansen
    */
    public class SmtpMailHandler
    {
        public SmtpMailHandler(Mail mail, string mailServer)
        {
            Mail = mail;
            MailServer = mailServer;
        }

        /// <summary>
        /// Sends an email using Google's SMTP server
        /// </summary>
        public void SendMail()
        {
            // Establish server connection
            var client = new SmtpClient
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = MailServer,
                Credentials = new NetworkCredential(Mail.UserName, Mail.Password)
            };

            // Construct MailMessage
            var message = new MailMessage(Mail.Sender, Mail.Receiver)
            {
                Subject = Mail.Subject,
                Body = Mail.Body + "\n\n" + Mail.Signature
            };

            client.Send(message);

            // Release the resources used by the MailMessage
            message.Dispose();
        }

        public Mail Mail { get; set; }
        public string MailServer { get; set; }
    }
}
