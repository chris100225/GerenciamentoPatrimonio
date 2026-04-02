using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.TipoAlteracaoDTO;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAlteracaoController : ControllerBase
    {
        private readonly TipoAlteracaoService _service;

        public TipoAlteracaoController(TipoAlteracaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarTipoAlteracaoDTO>> Listar()
        {
            List<ListarTipoAlteracaoDTO> tipos = _service.Listar();
            return Ok(tipos);
        }


        [HttpGet("{id}")]
        public ActionResult<ListarTipoAlteracaoDTO> BuscarPorID(Guid id)
        {
            try
            {
                ListarTipoAlteracaoDTO tipo = _service.BuscarPorID(id);

                return Ok(tipo);
            }
            catch (DomainException ex)
            {

                return NotFound(ex.Message);
            }

        }

        [HttpPost]

        public ActionResult Adicionar(CriarTipoAlteracaoDTO criarDTO)
        {
            try
            {
                _service.Adicionar(criarDTO);
                return Created();
            }
            catch (DomainException ex)
            {

                return BadRequest();
            }
        }


        [HttpPut("{id}")]

        public ActionResult Atualizar(Guid id, CriarTipoAlteracaoDTO dto)
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
