using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;

namespace ApiGerenciadorDeViagens.Servicos.Passagens;

public class Email //Classe com a funcionalidade de envio de emails
{
     public string Provedor { get; private set; }
     public string Username { get; private set; }
     public string Password { get; private set; }

        public Email(string provedor, string username, string password)
        {
            Provedor = provedor ?? throw new ArgumentNullException(nameof(provedor));
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public void Enviaremail(List<string> emailsTo, string subject, string body, List<string> attachments) //Método para pegar os dados informados e fazer o email do email
        {
            var message = PrepararMensagem(emailsTo, subject, body, attachments);

            EnviarEmailComSmtp(message);
        }

        private MailMessage PrepararMensagem(List<string> emailsTo, string subject, string body, List<string> attachments) //Método para preparar a mensagem para o envio com base nos dados informados
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(Username);

            foreach (var email in emailsTo)
            {
                if (ValidadorEmail(email))
                {
                    mail.To.Add(email);
                }
            }

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            foreach (var file in attachments)
            {
                var data = new Attachment(file, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(file);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file);

                mail.Attachments.Add(data);
            }

            return mail;
        }

        private bool ValidadorEmail(string email) //Método para validar o email informado
        {
            Regex expression = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
            if (expression.IsMatch(email))
                return true;

            return false;
        }

        private void EnviarEmailComSmtp(MailMessage message) //Me´todo configurado para fazer o envio do endereço de email configurado
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Host = Provedor;
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Timeout = 60*60;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(Username, Password);
            smtpClient.Send(message);
            smtpClient.Dispose();
        }
}
