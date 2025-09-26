using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal_dos_Agentes.Server.Services.Interfaces;
using System.Security.Claims;

namespace Portal_dos_Agentes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [Authorize(Roles = "user")]
        [HttpPost("{preco}")]
        public async Task<IActionResult> FazerPedido(decimal preco)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userId, out var id) == false)
                return BadRequest("Usuário não encontrado");

            var (sucess, msg, pedido) = await _pedidoService.FazerPedidoAsync(id, preco);
            return sucess ? Ok(pedido) : BadRequest(msg);
        }
        [Authorize(Roles = "adm")]
        [HttpGet]
        public async Task<IActionResult> GetAllPedidos() => Ok(await _pedidoService.GetAllAsync());

        [Authorize]
        [HttpGet("meuspedidos")]

        public async Task<IActionResult> GetMyPedidos()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userId, out var id) == false)
                return BadRequest("Usuário não encontrado");

            var pedidos = await _pedidoService.GetAllByUserAsync(id);
            if (pedidos is null || !pedidos.Any())
                return NotFound("Nenhum pedido encontrado para o usuário.");

            return Ok(pedidos);
        }

        [Authorize(Roles = "adm")]
        [HttpPut]
        public async Task<IActionResult> ConfirmarPedido(uint pedidoId)
        {
            var success = await _pedidoService.ConfirmPedidoAsync(pedidoId);
            return success != null ? Ok(new { IsAceite = true }) : BadRequest(new { IsAceite = false });
        }
    }
}
