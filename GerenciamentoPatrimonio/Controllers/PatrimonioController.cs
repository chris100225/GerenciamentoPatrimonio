using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.EnderecoDTO;
using GerenciamentoPatrimonio.DTO.PatrimonioDTO;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatrimonioController : ControllerBase
    {
        private readonly PatrimonioService _service;

        public PatrimonioController(PatrimonioService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<ListarPatrimonioDTO>> Listar()
        {
            List<ListarPatrimonioDTO> patrimonio = _service.Listar();
            return Ok(patrimonio);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<ListarPatrimonioDTO> BuscarPorID(Guid id)
        {
            try
            {
                ListarPatrimonioDTO patrimonio = _service.BuscarPorId(id);
                return Ok(patrimonio);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize(Roles = "Coordenador")]
        [HttpPost("importar-csv")]
        public ActionResult Adicionar(IFormFile arquivoCsv)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrWhiteSpace(usuarioIdClaim))
                {
                    return Unauthorized("Usuário não autenticado.");
                }

                Guid usuarioId = Guid.Parse(usuarioIdClaim);

                _service.Adicionar(arquivoCsv, usuarioId);

                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Coordenador")]
        [HttpPatch("{id}/status")]
        public ActionResult AtualizarStatus(Guid id, AtualizarStatusPatrimonio dto)
        {
            try
            {
                _service.AtualizarStatus(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
