using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.UsuarioDTO;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarUsuarioDTO> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<ListarUsuarioDTO> usuariosDTO = usuarios.Select(usuarios => new ListarUsuarioDTO
            {
                UsuarioID = usuarios.UsuarioID,
                NIF = usuarios.NIF,
                Nome = usuarios.Nome,
                RG = usuarios.RG,
                CPF = usuarios.CPF,
                CarteiraTrabalho = usuarios.CarteiraTrabalho,
                Email = usuarios.Email,
                Ativo = usuarios.Ativo,
                PrimeiroAcesso = usuarios.PrimeiroAcesso,
                EnderecoID = usuarios.EnderecoID,
                CargoID = usuarios.CargoID,
                TipoUsuarioID = usuarios.TipoUsuarioID,
            }).ToList();

            return usuariosDTO;
        }
    }
}
