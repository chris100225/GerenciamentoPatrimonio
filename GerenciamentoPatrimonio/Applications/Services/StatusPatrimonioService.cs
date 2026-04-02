using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.CargoDTO;
using GerenciamentoPatrimonio.DTO.StatusPatrimonioDTO;
using GerenciamentoPatrimonio.DTO.TipoUsuarioDTO;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class StatusPatrimonioService
    {
        private readonly IStatusPatrimonioRepository _repository;

        public StatusPatrimonioService(IStatusPatrimonioRepository repository)
        {
            _repository = repository;
        }
        public List<ListarStatusPatrimonioDTO> Listar()
        {
            List<StatusPatrimonio> statusP = _repository.Listar();

            List<ListarStatusPatrimonioDTO> statusDTO = statusP.Select(statusP => new ListarStatusPatrimonioDTO
            {
                StatusPatrimonioID = statusP.StatusPatrimonioID,
                NomeStatus = statusP.NomeStatus,

            }).ToList();

            return statusDTO;
        }


        public ListarStatusPatrimonioDTO BuscarPorID(Guid statusID)
        {
            StatusPatrimonio statusP = _repository.BuscarPorID(statusID);

            if (statusP == null)
            {
                throw new DomainException("Status de patrimonio não encontrado");
            }

            ListarStatusPatrimonioDTO statusDTO = new ListarStatusPatrimonioDTO
            {
                StatusPatrimonioID = statusP.StatusPatrimonioID,
                NomeStatus = statusP.NomeStatus,
            };
            return statusDTO;
        }


        public void Adicionar(CriarStatusPatrimonioDTO criarDTO)
        {
            Validar.ValidarNome(criarDTO.NomeStatus);

            StatusPatrimonio statusExiste = _repository.BuscarPorNome(criarDTO.NomeStatus);

            if (statusExiste != null)
            {
                throw new DomainException("Esse status de patrimonio já está cadastrado");
            }

            StatusPatrimonio statusP = new StatusPatrimonio
            {
                NomeStatus = criarDTO.NomeStatus
            };

            _repository.Adicionar(statusP);
        }



        public void Atualizar(Guid id, CriarStatusPatrimonioDTO attDTO)
        {
            Validar.ValidarNome(attDTO.NomeStatus);

            StatusPatrimonio statusBanco = _repository.BuscarPorID(id);

            if (statusBanco == null)
            {
                throw new DomainException("Status de patrimonio não encontrado");
            }

            StatusPatrimonio statusExiste = _repository.BuscarPorNome(attDTO.NomeStatus);

            if (statusExiste != null)
            {
                throw new DomainException("Esse status de patrimonio já está cadastrado");
            }

            statusBanco.NomeStatus = attDTO.NomeStatus;

            _repository.Atualizar(statusBanco);
        }
    }
}
