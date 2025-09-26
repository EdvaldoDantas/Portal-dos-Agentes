using AuthLibrary.Models;
using AuthLibrary.Services;
using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Models;
using Portal_dos_Agentes.Server.Repositories.Interfaces;
using Portal_dos_Agentes.Server.Services.Interfaces;
using Portal_dos_Agentes.Server.Services.Utilities;

namespace Portal_dos_Agentes.Server.Services.Services;

public class AuthService : IAuthService
{
    private readonly IGenericRepository<User> genericRepository;
    private readonly ITokenService _tokenService;
    private readonly IAuthRepository repository;

    public AuthService(IGenericRepository<User> genericRepository, ITokenService tokenService, IAuthRepository repository)
    {
        this.genericRepository = genericRepository;
        _tokenService = tokenService;
        this.repository = repository;
    }

    public async Task<TokenResponse?> GenerateNewToken(string refreshToken)
    {
        var userLogin = await repository.IsUserLoged(refreshToken);
        if (userLogin is null)
            return new TokenResponse { AccessToken = null, Expiration = null, Message = "Inv�lido,fa�a login", Success = false};

        if(userLogin.IsRevoked)
             return new TokenResponse { AccessToken = null, Expiration = null, Message = "Token revogado, fa�a login", Success = false };

        if (userLogin.ExpirationDate < DateTime.UtcNow)
            return new TokenResponse { AccessToken = null, Expiration = null, Message = "Login expirado, fa�a Login", Success = false };

        var user = await genericRepository.GetByIdAsync(userLogin.User.Id);
        if(user is null) return new TokenResponse { AccessToken = null, Expiration = null, Message = "user NotFound" , Success = false };

        var roles = new List<string>();
        if (user.Role != null)
            roles.Add(user.Role);
        
        var accessToken = _tokenService.GenerateToken(user, roles);
        return accessToken;
    }

    public async Task<LoginModel?> Login(LoginDTO loginDTO)
    {
        var agente = await genericRepository.GetByIdAsync(loginDTO.AgenteId);
        if (agente == null)
            return null;

        if (!HashHelper.VerifyHash(agente.Senha, loginDTO.senha))
            return null;

        var roles = new List<string>();
        if (agente.Role != null)
            roles.Add(agente.Role);


        var acessToken = _tokenService.GenerateToken(agente, roles);
        var refreshToken = _tokenService.GenerateRefreshToken(); 

        var userLogin = new UserLogins
        {
            ExpirationDate = DateTime.UtcNow.AddDays(15),
            IsRevoked = false,
            LoginDate = DateTime.UtcNow,
            User = agente,
            Token = refreshToken
        };
        if (!agente.IsEmailConfirmed) 
        {
            agente.IsEmailConfirmed = true;
        }
        await repository.CreateAsync(userLogin);
        return new LoginModel
        {
            AcessToken = acessToken.AccessToken ?? "Erro",
            RefreshToken = refreshToken
        };  
    }

    public async Task<(bool Success, string Message)> Logout(string refreshToken)
    {
        var userLogin = await repository.IsUserLoged(refreshToken);
        if (userLogin is null)
            return (false, "Token invalido");
        userLogin.IsRevoked = true;
        await repository.SaveChanges();
        return (true, "Logout feito com sucesso");
    }
}