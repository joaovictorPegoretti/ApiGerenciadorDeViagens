using System;
using System.Net;
using System.Net.Mail;

namespace ApiGerenciadorDeViagens.Servicos.Passagens;

public class EnvioDeEmail
{
    public static void Envio(string email)
    {
        MailMessage MessagemEmail = new MailMessage();
        try
        {
            var ClientSmpt = new SmtpClient("smtp.gmail.com",587);
            ClientSmpt.EnableSsl = true;
            ClientSmpt.Timeout = 60 * 60;
            ClientSmpt.UseDefaultCredentials = false;
            ClientSmpt.Credentials = new NetworkCredential("joaovictormpegoretti@gmail.com", "Jvmarca12@");

            MessagemEmail.From = new MailAddress("joaovictormpegoretti@gmail.com", "João Victor - Piloto"); // Aqui representa quem está mandando no caso o email e o nome para esse email
            MessagemEmail.Body = "Agradecemos pela preferencia e confiança no nosso trabalho.Segue em anexo sua passagem";
            MessagemEmail.Subject = "Sua Passagem chegou";
            MessagemEmail.IsBodyHtml = true;
            MessagemEmail.Priority = MailPriority.Normal;
            MessagemEmail.To.Add(email);

            ClientSmpt.Send(MessagemEmail);
        }
        catch (Exception ex)
        {
            return;
        }
    }
}
