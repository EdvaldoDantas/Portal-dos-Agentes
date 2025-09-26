using MimeKit;
using MailKit.Net.Smtp;
using Portal_dos_Agentes.Server.Services.Interfaces;
using Portal_dos_Agentes.Server.DTOs;
namespace Portal_dos_Agentes.Server.Services.Utilities
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            this.logger = logger;
        }

        public async Task<(bool success, string resp)> SendEmailAsync(EmailDTO model)
        {
            try
            {
                string myEmail = Environment.GetEnvironmentVariable("EMAIL") ?? throw new Exception("Não foi possível encontrar o emaik");
                string senha = Environment.GetEnvironmentVariable("SENHA") ?? throw new Exception("Não foi possível encontrar a senha");
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("cota sílva", myEmail));
                message.To.Add(new MailboxAddress(model.Nome, model.To));
                message.Subject = model.Subject;
                message.Body = new TextPart
                {
                    Text = $"Olá {model.Nome} \n\n" +
                    $"{model.Message}"
                };
                using (var smtp = new SmtpClient())
                {
                    await smtp.ConnectAsync("smtp.gmail.com", 587, false);
                    await smtp.AuthenticateAsync(myEmail, senha);
                    await smtp.SendAsync(message);
                    await smtp.DisconnectAsync(true);
                }
                logger.LogInformation("Email Enviado com sucesso");
                return (true, "O seu email foi enviado");
            }
            catch (Exception ex)
            {
                logger.LogError($"Erro ao enviar o email {ex.Message}");
                return(false, ex.Message);
            }
        }
    }
}
