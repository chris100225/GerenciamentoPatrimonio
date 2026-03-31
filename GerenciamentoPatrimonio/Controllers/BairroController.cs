using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.BairroDTO;
using GerenciamentoPatrimonio.DTO.LocalizacaoDTO;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BairroController : ControllerBase
    {

        private readonly BairroService _service;

        public BairroController(BairroService service)
        {
            _service = service;
        }

        [HttpGet]

        public ActionResult<List<ListarBairroDTO>> Listar()
        {
            List<ListarBairroDTO> bairros = _service.Listar();
            return Ok(bairros);
        }


        [HttpGet("{id}")]

        public ActionResult<ListarBairroDTO> BuscarPorID(Guid id)
        {
            try
            {
                ListarBairroDTO bairro = _service.BuscarPorID(id);

                return Ok(bairro);
            }
            catch (DomainException ex)
            {

                return NotFound(ex.Message);
            }

        }



        [HttpPost]
        public ActionResult Adicionar(CriarBairroDTO criarDTO)
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

        public ActionResult Atualizar(Guid id, CriarBairroDTO attDTO)
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
    }
}
