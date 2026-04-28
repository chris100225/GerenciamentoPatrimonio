using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.SolicitacaoTransferenciaDTO;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoTransferenciaController : ControllerBase
    {
        private readonly SolicitacaoTransferenciaService _service;

        public SolicitacaoTransferenciaController(SolicitacaoTransferenciaService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<ListarSolicitacaoTransferenciaDTO>> Listar()
        {
            List<ListarSolicitacaoTransferenciaDTO> solicitacoes = _service.Listar();
            return Ok(solicitacoes);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<ListarSolicitacaoTransferenciaDTO> BuscarPorID(Guid id)
        {
            try
            {
                ListarSolicitacaoTransferenciaDTO solicitacao = _service.BuscarPorID(id);
                return Ok(solicitacao);
            }
            catch (DomainException ex)
            {

                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Adicionar(CriarSolicitacaoTransferenciaDTO criarDTO)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrWhiteSpace(usuarioIdClaim)) { return Unauthorized("Usuário não encontrado"); }

                Guid usuarioID = Guid.Parse(usuarioIdClaim);

                _service.Adicionar(usuarioID, criarDTO);
                return Created();
            }
            catch (DomainException ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("{id}/responder")]

        public ActionResult Responder(Guid id, ResponderSolicitacaoTransferenciaDTO respDTO)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrWhiteSpace(usuarioIdClaim)) { return Unauthorized("Usuário não encontrado"); }

                Guid usuarioID = Guid.Parse(usuarioIdClaim);

                _service.Responder(id, usuarioID, respDTO);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
                            }
        }

    }
}
