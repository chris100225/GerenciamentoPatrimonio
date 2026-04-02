using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.TipoPatrimonioDTO;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class TipoPatrimonioService
    {
        private readonly ITipoPatrimonioRepository _repository;

        public TipoPatrimonioService(ITipoPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoPatrimonioDTO> Listar()
        {
            List<TipoPatrimonio> tipos = _repository.Listar();
            List<ListarTipoPatrimonioDTO> tiposDTO = tipos.Select(tipos => new ListarTipoPatrimonioDTO
            {
                TipoPatrimonioID = tipos.TipoPatrimonioID,
                NomeTipo = tipos.NomeTipo,
            }).ToList();

            return tiposDTO;
        }

        public ListarTipoPatrimonioDTO BuscarPorID(Guid tipoPatrimonioID)
        {
            TipoPatrimonio tipo = _repository.BuscarPorID(tipoPatrimonioID);

            if (tipo == null)
            {
                throw new DomainException("Tipo de patrimonio não encontrado");
            }

            ListarTipoPatrimonioDTO tipoDTO = new ListarTipoPatrimonioDTO
            {
                TipoPatrimonioID = tipo.TipoPatrimonioID,
                NomeTipo = tipo.NomeTipo,
            };
            return tipoDTO;
        }

        public void Adicionar(CriarTipoPatrimonioDTO criarDTO)
        {
            Validar.ValidarNome(criarDTO.NomeTipo);

            TipoPatrimonio tipoExiste = _repository.BuscarPorNome(criarDTO.NomeTipo);

            if (tipoExiste != null)
            {
                throw new DomainException("Esse tipo de patrimonio já está cadastrado");
            }

            TipoPatrimonio tipo = new TipoPatrimonio
            {
                NomeTipo = criarDTO.NomeTipo
            };

            _repository.Adicionar(tipo);
        }

        public void Atualizar(Guid id, CriarTipoPatrimonioDTO attDTO)
        {
            Validar.ValidarNome(attDTO.NomeTipo);

            TipoPatrimonio tipoBanco = _repository.BuscarPorID(id);

            if (tipoBanco == null)
            {
                throw new DomainException("Tipo de patrimonio não encontrado");
            }

            TipoPatrimonio tipoExiste = _repository.BuscarPorNome(attDTO.NomeTipo);

            if (tipoExiste != null)
            {
                throw new DomainException("Esse tipo de patrimonio já está cadastrado");
            }

            tipoBanco.NomeTipo = attDTO.NomeTipo;

            _repository.Atualizar(tipoBanco);
        }
    }
}
