using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.LogPatrimonioDTO;
using GerenciamentoPatrimonio.DTO.PatrimonioDTO;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogPatrimonioController : ControllerBase
    {
        private readonly LogPatrimonioService _service;

        public LogPatrimonioController(LogPatrimonioService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<ListarLogPatrimonioDTO>> Listar()
        {
            List<ListarLogPatrimonioDTO> logs = _service.Listar();
            return Ok(logs);
        }

        [Authorize]
        [HttpGet("patrimonio/{id}")]

        public ActionResult<List<ListarLogPatrimonioDTO>> BuscarPorPatrimonio(Guid id)
        {
            try
            {
                List<ListarLogPatrimonioDTO> logs = _service.BuscarPorPatrimonio(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return NotFound();
            }
        }
    }
}
