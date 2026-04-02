using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.TipoAlteracaoDTO;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class TipoAlteracaoService
    {
        private readonly ITipoAlteracaoRepository _repository;

        public TipoAlteracaoService(ITipoAlteracaoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoAlteracaoDTO> Listar()
        {
            List<TipoAlteracao> tipos = _repository.Listar();
            List<ListarTipoAlteracaoDTO> tiposDTO = tipos.Select(tipos => new ListarTipoAlteracaoDTO
            {
                TipoAlteracaoID = tipos.TipoAlteracaoID,
                NomeTipo = tipos.NomeTipo,
            }).ToList();

            return tiposDTO;
        }

        public ListarTipoAlteracaoDTO BuscarPorID(Guid tipoAlteracaoID)
        {
            TipoAlteracao tipo = _repository.BuscarPorID(tipoAlteracaoID);

            if (tipo == null)
            {
                throw new DomainException("Tipo de alteração não encontrado");
            }

            ListarTipoAlteracaoDTO tipoDTO = new ListarTipoAlteracaoDTO
            {
                TipoAlteracaoID = tipo.TipoAlteracaoID,
                NomeTipo = tipo.NomeTipo,
            };
            return tipoDTO;
        }

        public void Adicionar(CriarTipoAlteracaoDTO criarDTO)
        {
            Validar.ValidarNome(criarDTO.NomeTipo);

            TipoAlteracao tipoExiste = _repository.BuscarPorNome(criarDTO.NomeTipo);

            if (tipoExiste != null)
            {
                throw new DomainException("Esse tipo de alteração já está cadastrado");
            }

            TipoAlteracao tipo = new TipoAlteracao
            {
                NomeTipo = criarDTO.NomeTipo
            };

            _repository.Adicionar(tipo);
        }

        public void Atualizar(Guid id, CriarTipoAlteracaoDTO attDTO)
        {
            Validar.ValidarNome(attDTO.NomeTipo);

            TipoAlteracao tipoBanco = _repository.BuscarPorID(id);

            if (tipoBanco == null)
            {
                throw new DomainException("Tipo de alteração não encontrado");
            }

            TipoAlteracao tipoExiste = _repository.BuscarPorNome(attDTO.NomeTipo);

            if (tipoExiste != null)
            {
                throw new DomainException("Esse tipo de alteração já está cadastrado");
            }

            tipoBanco.NomeTipo = attDTO.NomeTipo;

            _repository.Atualizar(tipoBanco);
        }
    }
}
