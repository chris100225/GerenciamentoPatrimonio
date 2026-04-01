using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.CargoDTO;
using GerenciamentoPatrimonio.DTO.StatusPatrimonioDTO;
using GerenciamentoPatrimonio.DTO.TipoUsuarioDTO;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusPatrimonioController : ControllerBase
    {
        private readonly StatusPatrimonioService _service;

        public StatusPatrimonioController(StatusPatrimonioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarStatusPatrimonioDTO>> Listar()
        {
            List<ListarStatusPatrimonioDTO> statusP = _service.Listar();
            return Ok(statusP);
        }


        [HttpGet("{id}")]
        public ActionResult<ListarStatusPatrimonioDTO> BuscarPorID(Guid id)
        {
            try
            {
                ListarStatusPatrimonioDTO statusP = _service.BuscarPorID(id);

                return Ok(statusP);
            }
            catch (DomainException ex)
            {

                return NotFound(ex.Message);
            }

        }

        [HttpPost]

        public ActionResult Adicionar(CriarStatusPatrimonioDTO criarDTO)
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
    }
}
