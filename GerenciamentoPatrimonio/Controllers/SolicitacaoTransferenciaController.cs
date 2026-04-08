using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.SolicitacaoTransferenciaDTO;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult <List<ListarSolicitacaoTransferenciaDTO>> Listar()
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
    }
}
