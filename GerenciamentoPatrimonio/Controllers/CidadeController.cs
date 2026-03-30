using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.AreaDTO;
using GerenciamentoPatrimonio.DTO.CidadeDTO;
using GerenciamentoPatrimonio.DTO.LocalizacaoDTO;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly CidadeService _service;

        public CidadeController(CidadeService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarCidadeDTO>> Listar()
        {
            List<ListarCidadeDTO> cidades = _service.Listar();
            return Ok(cidades);
        }

        [HttpGet("{id}")]

        public ActionResult<ListarCidadeDTO> BuscarPorId(Guid id)
        {
            try
            {
                ListarCidadeDTO cidade = _service.BuscarPorId(id);
                return Ok(cidade);
            }
            catch (DomainException ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPost]

        public ActionResult Adicionar(CriarCidadeDTO criarCidade)
        {
            try
            {
                _service.Adicionar(criarCidade);
                return Created();
            }
            catch (DomainException ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]

        public ActionResult Atualizar(Guid id, CriarCidadeDTO attCidade)
        {
            try
            {
                _service.Atualizar(id, attCidade);
                return NoContent();
            }
            catch (DomainException ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
