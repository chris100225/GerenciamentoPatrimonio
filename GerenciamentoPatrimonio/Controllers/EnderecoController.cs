using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.EnderecoDTO;
using GerenciamentoPatrimonio.DTO.LocalizacaoDTO;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {


        private readonly EnderecoService _service;

        public EnderecoController(EnderecoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarEnderecoDTO>> Listar()
        {
            List<ListarEnderecoDTO> enderecos = _service.Listar();
            return Ok(enderecos);
        }


        [HttpGet("{id}")]
        public ActionResult<ListarEnderecoDTO> BuscarPorID(Guid id)
        {
            try
            {
                ListarEnderecoDTO enderecos = _service.BuscarPorID(id);
                return Ok(enderecos);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarEnderecoDTO criarDTO)
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
    }
}
