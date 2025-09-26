using AuthLibrary.Models;
using Portal_dos_Agentes.Server.DTOs;

namespace Portal_dos_Agentes.Server.Services.Interfaces;

public interface IAuthService
{
    Task<LoginModel?> Login(LoginDTO loginDTO); 
    Task<TokenResponse?> GenerateNewToken(string refreshToken);

    Task<(bool Success, string Message)> Logout(string refreshToken);
}