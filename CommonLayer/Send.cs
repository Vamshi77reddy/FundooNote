﻿using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace CommonLayer
{
    public class Send
    {
        public string SendingMail(string emailTo, string token)
        {
            try
            {
                string emailFrom = "vamshireddy6563@gmail.com";

                MailMessage message = new MailMessage(emailFrom, emailTo);
                string mailbody = "Token Generated : " + token;
                message.Subject = "Genereted Token will expire after 15 minute";
                message.Body = mailbody.ToString();
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                System.Net.NetworkCredential credential = new System.Net.NetworkCredential("vamshireddy6563@gmail.com", "okjf tftk owbe bsxp");

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = credential;
                smtpClient.Send(message);
                return emailTo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
