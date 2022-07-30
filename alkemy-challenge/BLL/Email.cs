using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace alkemy_challenge.BLL
{
    public class Email : IEmail
    {
        private string _server;
        private string _from;
        private string _passwordOrToken;
        private string _port;


        public Email(IConfiguration Configuration)
        {
            _server = Configuration["Email:server"];
            _from = Configuration["Email:from"];
            _passwordOrToken = Configuration["Email:passwordOrToken"];
            _port = Configuration["Email:port"];
        }

        /// <summary>
        /// Enviar un email
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="to"></param>
        /// <param name="isHtml"></param>
        public void SendEmail(string body, string subject, string to, string displayName ,bool isHtml = true)
        {
            try
            {
                //para gmail
                MailMessage message = new MailMessage(_from, to, subject, body);
                message.From = new MailAddress(_from, displayName);
                message.IsBodyHtml = isHtml;
                message.Body = body;

                SmtpClient client = new SmtpClient(_server);
                client.EnableSsl = true;
                client.Port = Int32.Parse(_port);
                client.Credentials = new System.Net.NetworkCredential(_from, _passwordOrToken);
                //esto puede ir async y notificacion
                client.Send(message);
            }
            catch (Exception ex)
            {
                //log ex
                throw new Exception("No se pudo enviar el email", ex);
            }
            finally
            {
                //log
                //email enviado
            }
        }

    }

}

