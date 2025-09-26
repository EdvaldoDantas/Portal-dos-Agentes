using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Services.Interfaces;
using System.Security.Claims;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portal_dos_Agentes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _services;

        public UserController(IUserService services)
        {
            _services = services;
        }


        // GET: api/<SubAgentesController>
        [Authorize(Roles = "adm")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           return Ok(await _services.GetAll());
        }

        // GET api/<SubAgentesController>/5
        [Authorize]
        [HttpGet("userData")]
        public async Task<IActionResult> GetMyData()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var subAgente = await _services.GetByIdAsync(int.Parse(id));
            if(subAgente is not null)
                return Ok(subAgente);
            return Unauthorized("Não autorizado");
        }

        // POST api/<SubAgentesController>

        [Authorize(Roles = "adm")]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] SubAgentDTO subAgente)
        {
            var (success, msg, subagente) = await _services.AddSubAgenteAsync(subAgente);
            if (!success)
                return BadRequest(msg);
            return Ok(new {dados = subagente, senha = msg});
        }

        // PUT api/<SubAgentesController>/5
        [Authorize(Roles = "adm")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UserDTO userDto, int id)
        {
            var userUpdated = await _services.UpdateAsync(id, userDto);
            return userUpdated is not null ? Ok(userUpdated) : NotFound("Usuário não encontrado ou erro ao atualizar");
        }

        // DELETE api/<SubAgentesController>/5
        [Authorize(Roles = "adm")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (success, msg) = await _services.Delete(id);
            if (!success)
                return NotFound(msg);
            return Ok(msg);
        }
    }
}
