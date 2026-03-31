using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.UsuarioDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarUsuarioDTO>> Listar()
        {
            List<ListarUsuarioDTO> usuarios = _service.Listar();
            return Ok(usuarios);
        }
    }
}
