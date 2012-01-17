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
    public class SendEmail
    {
        public SendEmail()
        {

        }

        public void EnviarEmail(string from, string to, string bcc, string cc, string subject, string body)
        {
            // Criar uma nova instância do objecto
            MailMessage mMailMessage = new MailMessage();
            mMailMessage.From = new MailAddress(from);
            mMailMessage.To.Add(new MailAddress(to));
            if ((bcc != null) && (bcc != string.Empty))
            {
                mMailMessage.Bcc.Add(new MailAddress(bcc));
            }

            if ((cc != null) && (cc != string.Empty))
            {
                mMailMessage.CC.Add(new MailAddress(cc));
            }
            mMailMessage.Subject = subject;

            mMailMessage.Body = body;
            mMailMessage.IsBodyHtml = true;
            mMailMessage.Priority = MailPriority.Normal;

            // Instantiate a new instance of SmtpClient
            SmtpClient mSmtpClient = new SmtpClient("smtp.gmail.com", 587);
            //SmtpClient mSmtpClient = new SmtpClient();
            //mSmtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
            mSmtpClient.Credentials = new System.Net.NetworkCredential("lusofonosonline@gmail.com", "AntonioPaulo");
            //mSmtpClient.UseDefaultCredentials = false;
            mSmtpClient.EnableSsl = true;

            mSmtpClient.Send(mMailMessage);

        }
    }
}



