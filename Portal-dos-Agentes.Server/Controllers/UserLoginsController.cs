using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal_dos_Agentes.Server.Services.Interfaces;

namespace Portal_dos_Agentes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "adm")]
    public class UserLoginsController : ControllerBase
    {
        private readonly IUserLoginsService _service;

        public UserLoginsController(IUserLoginsService service)
        {
            _service = service;
        }
        [HttpGet("/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetUserLogins(id);
            if (result != null)
                return Ok(result);
            return NotFound("Login não encontrado");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllUserLogin();
            if (result != null)
                return Ok(result);
            return NotFound("Login não encontrado");
        }
        [HttpGet("AllLogins")]
        public async Task<IActionResult> GetAllLogins() => Ok(await _service.GetAll());

        [HttpDelete("RemoveAll")]
        public async Task<IActionResult> RemoveAll()
        {
            await _service.RemoveAll();
            return Ok("Todos os logins foram removidos com sucesso");
        }
    }
}
