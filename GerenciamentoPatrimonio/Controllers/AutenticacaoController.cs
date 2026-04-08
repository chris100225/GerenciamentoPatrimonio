using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTO.AutenticacaoDTO;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutenticacaoService _service;

        public AutenticacaoController(AutenticacaoService service)
        {
            _service = service;
        }

        [HttpPost("login")]

        public ActionResult<TokenDTO> Login(LoginDTO loginDTO)
        {
            try
            {
                TokenDTO token = _service.Login(loginDTO);
                return Ok(token);
            }
            catch (DomainException ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("trocar-senha")]

        public ActionResult TrocarPrimeiraSenha(TrocarPrimeiraSenhaDTO trocarDTO)
        {
            try
            {
                string usuarioIDClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrWhiteSpace(usuarioIDClaim))
                {
                    return Unauthorized("Usuário não autenticado");
                }

                Guid usuarioID = Guid.Parse(usuarioIDClaim);

                _service.TrocarPrimeiraSenha(usuarioID, trocarDTO);
                return NoContent();
            }
            catch (DomainException ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
