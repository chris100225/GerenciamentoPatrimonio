using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.EnderecoDTO;
using GerenciamentoPatrimonio.DTO.PatrimonioDTO;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        //[Authorize]
        [HttpGet]
        public ActionResult<List<ListarPatrimonioDTO>> Listar()
        {
            List<ListarPatrimonioDTO> patrimonio = _service.Listar();
            return Ok(patrimonio);
        }

        //[Authorize]
        [HttpGet("{id}")]
        public ActionResult<ListarPatrimonioDTO> BuscarPorID(Guid id)
        {
            try
            {
                ListarPatrimonioDTO patrimonio = _service.BuscarPorID(id);
                return Ok(patrimonio);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[Authorize(Roles = "Coordenador")]
        [HttpPost]
        public ActionResult Adicionar(CriarPatrimonioDTO criarDTO)
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

        //[Authorize(Roles = "Coordenador")]
        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarPatrimonioDTO attDTO)
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

        //[Authorize(Roles = "Coordenador")]
        [HttpPatch("{id}/status")]
        public ActionResult AtualizarStatus(Guid id, CriarPatrimonioDTO attDTO)
        {
            try
            {
                _service.AtualizarStatus(id, attDTO);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
