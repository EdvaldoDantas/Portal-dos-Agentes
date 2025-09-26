using Portal_dos_Agentes.Server.DTOs;

namespace Portal_dos_Agentes.Server.Services.Interfaces
{
    public interface IEmailSender
    {
        Task<(bool success, string resp)> SendEmailAsync(EmailDTO model);
    }
}