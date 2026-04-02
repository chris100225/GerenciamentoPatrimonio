using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.TipoPatrimonioDTO;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPatrimonioController : ControllerBase
    {
        private readonly TipoPatrimonioService _service;

        public TipoPatrimonioController(TipoPatrimonioService service)
        {
            _service = service;
        }


        [HttpGet]
        public ActionResult<List<ListarTipoPatrimonioDTO>> Listar()
        {
            List<ListarTipoPatrimonioDTO> tipos = _service.Listar();
            return Ok(tipos);
        }


        [HttpGet("{id}")]
        public ActionResult<ListarTipoPatrimonioDTO> BuscarPorID(Guid id)
        {
            try
            {
                ListarTipoPatrimonioDTO tipo = _service.BuscarPorID(id);

                return Ok(tipo);
            }
            catch (DomainException ex)
            {

                return NotFound(ex.Message);
            }

        }

        [HttpPost]

        public ActionResult Adicionar(CriarTipoPatrimonioDTO criarDTO)
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

        public ActionResult Atualizar(Guid id, CriarTipoPatrimonioDTO dto)
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
