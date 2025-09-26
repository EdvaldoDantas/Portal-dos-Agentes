using Microsoft.AspNetCore.Mvc;
using Portal_dos_Agentes.Server.Repositories.Interfaces;
namespace Portal_dos_Agentes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        public readonly IDashBoardRepository _dashBoardRepository;

        public DashBoardController(IDashBoardRepository dashBoardRepository)
        {
            _dashBoardRepository = dashBoardRepository;
        }
        [HttpGet("GetTotal")]
        public async Task<IActionResult> GetTotal()
        {
            var (total, variacao) = await _dashBoardRepository.GetTotal();
            return Ok(new { total, variacao });
        }
        [HttpGet("GetRankingMes")]
        public async Task<IActionResult> GetRankingMes()
        {
            var ranking = await _dashBoardRepository.GetRankingMes();
            return Ok(ranking);
        }
        [HttpGet("TotalAgentes")]
        public async Task<IActionResult> TotalAgentes()
        {
            var total = await _dashBoardRepository.TotalAgentes();
            return Ok(new { total });
        }
        [HttpGet("AgentesNovos")]
        public async Task<IActionResult> AgentesNovos()
        {
            var (mesAtual, percentual) = await _dashBoardRepository.AgentesNovos();
            return Ok(new { mesAtual, percentual });
        }
        [HttpGet("DashboardDadosMes")]
        public async Task<IActionResult> DashboardDadosMes()
        {
            var dados = await _dashBoardRepository.DashboardDadosMes();
            return Ok(dados);
        }
    }
}
