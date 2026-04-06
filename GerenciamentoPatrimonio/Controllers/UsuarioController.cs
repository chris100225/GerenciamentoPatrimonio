using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.UsuarioDTO;
using GerenciamentoPatrimonio.Exceptions;
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

        [HttpGet("{id}")]

        public ActionResult<ListarUsuarioDTO> BuscarPorID(Guid id)
        {
            try
            {
                ListarUsuarioDTO usuario = _service.BuscarPorID(id);
                return Ok(usuario);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
                throw;
            }
        }

        [HttpPost]

        public ActionResult Adicionar(CriarUsuarioDTO criarDTO)
        {
            try
            {
                _service.Adicionar(criarDTO);
                return Created();
            }
            catch (DomainException ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]

        public ActionResult Atualizar(Guid id, CriarUsuarioDTO attDTO)
        {
            try
            {
                _service.Atualizar(id, attDTO);
                return NoContent();
            }
            catch (DomainException ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/status")]

        public ActionResult AtualizarStatus(Guid id, AtualizarStatusUsuarioDTO attStatusDTO)
        {
            try
            {
                _service.AtualizarStatus(id, attStatusDTO);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
    }
}