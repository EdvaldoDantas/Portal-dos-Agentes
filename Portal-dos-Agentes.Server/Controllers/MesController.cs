using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal_dos_Agentes.Server.Services.Interfaces;
using System.Security.Claims;

namespace Portal_dos_Agentes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesController : ControllerBase
    {
        private readonly IMesService mesService;
        public MesController(IMesService mesService)
        {
            this.mesService = mesService;
        }
        [HttpGet]
        public async Task<IActionResult> GetMesesAsync()
        {
            var meses = await mesService.GetMesesAsync();
            return Ok(meses);
        }
        [Authorize(Roles = "user")]
        [HttpGet("GetMesesByUserAsync")]
        public async Task<IActionResult> GetMesesByUserAsync()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var meses = await mesService.GetMesesByUserAsync(userId);
            return Ok(meses);
        }
        [Authorize(Roles = "adm")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var relatorios = await mesService.GetMesesByUserAsync(id);
            if (relatorios is null || relatorios.Count == 0)
            {
                return NotFound("Nenhum mês encontrado para este usuário.");
            }
            return Ok(relatorios);
        }
        [HttpGet("GetByDate")]
        public async Task<IActionResult> GetByDate(int mes, int ano, int userId)
        {
            if (mes < 1 || mes > 12 || ano < 1)
            {
                return BadRequest("Mês ou ano inválido.");
            }
            var relatorios = await mesService.GetDatByDate(userId, mes, ano);
            if (relatorios is null || relatorios.Count == 0)
            {
                return NotFound("Nenhum relatório encontrado para esta data.");
            }
            return Ok(relatorios);
        }
        [HttpGet("GetByMesAndDay")]
        public async Task<IActionResult> GetByMesAndDay(int ano, int mes, int dia)
        {
            if (mes < 1 || mes > 12 || ano < 1 || dia < 1 || dia > DateTime.DaysInMonth(ano, mes))
            {
                return BadRequest("Mês, ano ou dia inválido.");
            }
            var dias = await mesService.GetByMesAndDay(ano, mes, dia);
            if (dias is null || dias.Count == 0)
            {
                return NotFound("Nenhum relatório encontrado para esta data.");
            }
            return Ok(dias);
        }
        [HttpGet("GetDaysMes")]
        public async Task<IActionResult> GetDaysMes(int ano, int mes)
        {
            if (mes < 1 || mes > 12 || ano < 1)
            {
                return BadRequest("Mês ou ano inválido.");
            }
            return Ok(await mesService.GetAllDaysMes(ano, mes));
        }
    }
}
