using Microsoft.AspNetCore.Mvc;
using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Services.Interfaces;

namespace Portal_dos_Agentes.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    private readonly IAuthManager _authManager;

    public AuthController(IAuthService auth, IAuthManager authManager)
    {
        _auth = auth;
        _authManager = authManager;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var result = await _auth.Login(loginDTO);
        if (result != null)
        {
            await _authManager.RemoveLastLogin(loginDTO.AgenteId);
            return Ok(result);
        }
        return Unauthorized("Credenciais inválidas");
    }

    [HttpPost("GetToken")]
    public async Task<IActionResult> GetToken([FromBody] RefreshTokenDTO model)
    {
        var acessToken = await _auth.GenerateNewToken(model.RefreshToken);
        if (acessToken != null)
            if (acessToken.Success)
                return Ok(acessToken);
            else return BadRequest(new { Sucess = acessToken.Success, message = acessToken.Message });
        return BadRequest("Erro inesperado");
    }
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(string refreshToken)
    {
        var result = await _auth.Logout(refreshToken);
        if (result.Success)
            return Ok(result.Message);
        return BadRequest(result.Message);
    }
}