using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.AreaDTO;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly AreaService _service;

        public AreaController(AreaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarAreaDTO>> Listar()
        {
            List<ListarAreaDTO> areas = _service.Listar();
            return Ok(areas);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarAreaDTO> BuscarPorID(Guid id)
        {
            try
            {
                ListarAreaDTO area = _service.BuscarPorID(id);

                return Ok(area);
            }
            catch (DomainException ex)
            {

                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        public ActionResult Adicionar(CriarAreaDTO dto)
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

        public ActionResult Atualizar(Guid id, CriarAreaDTO dto) {
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
