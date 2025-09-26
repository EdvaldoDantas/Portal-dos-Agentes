using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal_dos_Agentes.Server.Repositories.Interfaces;

namespace Portal_dos_Agentes.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashBoardAgenteController : ControllerBase
    {
        public readonly IDashBoardAgenteRepository _dashBoardAgenteRepository;

        public DashBoardAgenteController(IDashBoardAgenteRepository dashBoardAgenteRepository)
        {
            _dashBoardAgenteRepository = dashBoardAgenteRepository;
        }
        [Authorize(Roles = "user")]
        [HttpGet("GetTotal")]
        public async Task<IActionResult> GetTotal()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("ID do usuário não encontrado");
            var (total, variacao) = await _dashBoardAgenteRepository.GetTotal(int.Parse(id));
            return Ok(new { total, variacao });
        }
        [Authorize(Roles = "user")]
        [HttpGet("GetRankingMes")]
        public async Task<IActionResult> GetRankingMes()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("ID do usuário não encontrado");
            var ranking = await _dashBoardAgenteRepository.GetRankingMes(int.Parse(id));
            return Ok(ranking);
        }
        [Authorize(Roles = "user")]
        [HttpGet("DashboardDadosMes")]
        public async Task<IActionResult> DashboardDadosMes()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("ID do usuário não encontrado");
            var dados = await _dashBoardAgenteRepository.DashboardDadosMes(int.Parse(id));
            return Ok(dados);
        }
    }
}
