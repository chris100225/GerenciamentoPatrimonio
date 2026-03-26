using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.AreaDTO;
using GerenciamentoPatrimonio.DTO.LocalizacaoDTO;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        private readonly LocalizacaoService _service;

        public LocalizacaoController(LocalizacaoService service)
        {
            _service = service;
        }

        [HttpGet]

        public ActionResult<List<ListarLocalizacaoDTO>> Listar()
        {
            List<ListarLocalizacaoDTO> localizacoes = _service.Listar();
            return Ok(localizacoes);
        }


        [HttpGet("{id}")]

        public ActionResult<ListarLocalizacaoDTO> BuscarPorId(Guid id)
        {
            try
            {
                ListarLocalizacaoDTO localizacao = _service.BuscarPorId(id);

                return Ok(localizacao);
            }
            catch (DomainException ex)
            {

                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        public ActionResult Adicionar(CriarLocalizacaoDTO dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]

        public ActionResult Atualizar(Guid id, CriarLocalizacaoDTO dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
