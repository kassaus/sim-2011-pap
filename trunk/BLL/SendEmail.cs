// -----------------------------------------------------------------------
// <copyright file="SendEmail.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Net.Mail;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class SendEmail
    {
        public static void EnviarEmail(string to, string subject, string body)
        {
            MailMessage mMailMessage = new MailMessage();
            mMailMessage.To.Add(new MailAddress(to));
            mMailMessage.Subject = subject;

            mMailMessage.Body = body;
            mMailMessage.IsBodyHtml = true;
            mMailMessage.Priority = MailPriority.Normal;

            // Instantiate a new instance of SmtpClient
            SmtpClient mSmtpClient = new SmtpClient();          
            mSmtpClient.Send(mMailMessage);

        }
    }
}



