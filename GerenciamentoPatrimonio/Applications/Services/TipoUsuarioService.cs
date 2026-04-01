using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.AreaDTO;
using GerenciamentoPatrimonio.DTO.TipoUsuarioDTO;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class TipoUsuarioService
    {
        private readonly ITipoUsuarioRepository _repository;

        public TipoUsuarioService(ITipoUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoUsuarioDTO> Listar()
        {
            List<TipoUsuario> tipos = _repository.Listar();
            List<ListarTipoUsuarioDTO> tiposDTO = tipos.Select(tipos => new ListarTipoUsuarioDTO
            {
                TipoUsuarioID = tipos.TipoUsuarioID,
                NomeTipo = tipos.NomeTipo,
            }).ToList();

            return tiposDTO;
        }
        public ListarTipoUsuarioDTO BuscarPorID(Guid tipoUsuarioID)
        {
            TipoUsuario tipo = _repository.BuscarPorID(tipoUsuarioID);

            if (tipo == null)
            {
                throw new DomainException("Tipo de usuário não encontrado");
            }

            ListarTipoUsuarioDTO tipoDTO = new ListarTipoUsuarioDTO
            {
                TipoUsuarioID = tipo.TipoUsuarioID,
                NomeTipo = tipo.NomeTipo,
            };
            return tipoDTO;
        }

        public void Adicionar(CriarTipoUsuarioDTO criarDTO)
        {
            Validar.ValidarNome(criarDTO.NomeTipo);

            TipoUsuario tipoExiste = _repository.BuscarPorNome(criarDTO.NomeTipo);

            if (tipoExiste != null)
            {
                throw new DomainException("Esse tipo de usuário já está cadastrado");
            }

            TipoUsuario tipo = new TipoUsuario
            {
                NomeTipo = criarDTO.NomeTipo
            };

            _repository.Adicionar(tipo);
        }

        public void Atualizar(Guid id, CriarTipoUsuarioDTO attDTO)
        {
            Validar.ValidarNome(attDTO.NomeTipo);

            TipoUsuario tipoBanco = _repository.BuscarPorID(id);

            if (tipoBanco == null)
            {
                throw new DomainException("Tipo de usuário não encontrado");
            }

            TipoUsuario tipoExiste = _repository.BuscarPorNome(attDTO.NomeTipo);

            if (tipoExiste != null)
            {
                throw new DomainException("Esse tipo de usuário já está cadastrado");
            }

            tipoBanco.NomeTipo = attDTO.NomeTipo;

            _repository.Atualizar(tipoBanco);
        }
    }
}
