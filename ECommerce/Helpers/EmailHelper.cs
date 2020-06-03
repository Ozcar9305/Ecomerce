﻿
namespace ECommerce.Helpers
{
    using ECommerceDataModel;
    using System;
    using System.Net.Mail;

    public class EmailHelper
    {
        /// <summary>
        /// Permite el envio de correos electronicos
        /// </summary>
        ///<param name="mailOptions">Opciones de correo electronico</param>
        public void SendEmail(MailDTO mailOptions)
        {
            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress(mailOptions.EmailFrom);
                mail.To.Add(mailOptions.EmailTo);
                mail.Subject = mailOptions.EmailSubject;
                mail.IsBodyHtml = true;
                mail.Body = mailOptions.EmailBody;

                SmtpClient SmtpServer = new SmtpClient(mailOptions.SmtpServer);
                SmtpServer.Port = mailOptions.Port;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mailOptions.SmtpUser, mailOptions.SmtpPassword);
                SmtpServer.EnableSsl = mailOptions.EnableSsl;
                SmtpServer.Send(mail);
            }
            catch (Exception exception)
            {
                exception.LogException();
            }
        }
    }
}
