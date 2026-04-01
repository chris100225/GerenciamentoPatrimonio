using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.CargoDTO;
using GerenciamentoPatrimonio.DTO.TipoUsuarioDTO;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly CargoService _service;

        public CargoController(CargoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarCargoDTO>> Listar()
        {
            List<ListarCargoDTO> cargo = _service.Listar();
            return Ok(cargo);
        }


        [HttpGet("{id}")]
        public ActionResult<ListarCargoDTO> BuscarPorID(Guid id)
        {
            try
            {
                ListarCargoDTO cargo = _service.BuscarPorID(id);

                return Ok(cargo);
            }
            catch (DomainException ex)
            {

                return NotFound(ex.Message);
            }

        }


        [HttpPost]

        public ActionResult Adicionar(CriarCargoDTO criarDTO)
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

        public ActionResult Atualizar(Guid id, CriarCargoDTO attDTO)
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
