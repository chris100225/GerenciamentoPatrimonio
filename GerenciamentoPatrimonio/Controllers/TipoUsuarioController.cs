using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.AreaDTO;
using GerenciamentoPatrimonio.DTO.TipoUsuarioDTO;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly TipoUsuarioService _service;

        public TipoUsuarioController(TipoUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarTipoUsuarioDTO>> Listar()
        {
            List<ListarTipoUsuarioDTO> tipos = _service.Listar();
            return Ok(tipos);
        }


        [HttpGet("{id}")]
        public ActionResult<ListarTipoUsuarioDTO> BuscarPorID(Guid id)
        {
            try
            {
                ListarTipoUsuarioDTO tipo = _service.BuscarPorID(id);

                return Ok(tipo);
            }
            catch (DomainException ex)
            {

                return NotFound(ex.Message);
            }

        }

        [HttpPost]

        public ActionResult Adicionar(CriarTipoUsuarioDTO criarDTO)
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

        public ActionResult Atualizar(Guid id, CriarTipoUsuarioDTO dto)
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
