using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.StatusTransferenciaDTO;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusTransferenciaController : ControllerBase
    {
        private readonly StatusTransferenciaService _service;

        public StatusTransferenciaController(StatusTransferenciaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarStatusTransferenciaDTO>> Listar()
        {
            List<ListarStatusTransferenciaDTO> statusT = _service.Listar();
            return Ok(statusT);
        }


        [HttpGet("{id}")]
        public ActionResult<ListarStatusTransferenciaDTO> BuscarPorID(Guid id)
        {
            try
            {
                ListarStatusTransferenciaDTO statusT = _service.BuscarPorID(id);

                return Ok(statusT);
            }
            catch (DomainException ex)
            {

                return NotFound(ex.Message);
            }

        }


        [HttpPost]

        public ActionResult Adicionar(CriarStatusTransferenciaDTO criarDTO)
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

        public ActionResult Atualizar(Guid id, CriarStatusTransferenciaDTO attDTO)
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
