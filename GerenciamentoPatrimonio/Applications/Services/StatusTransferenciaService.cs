using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.StatusTransferenciaDTO;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class StatusTransferenciaService
    {
        private readonly IStatusTansferenciaRepository _repository;

        public StatusTransferenciaService(IStatusTansferenciaRepository repository)
        {
            _repository = repository;
        }
        public List<ListarStatusTransferenciaDTO> Listar()
        {
            List<StatusTransferencia> statusT = _repository.Listar();

            List<ListarStatusTransferenciaDTO> statusDTO = statusT.Select(statusT => new ListarStatusTransferenciaDTO
            {
                StatusTransferenciaID = statusT.StatusTransferenciaID,
                NomeStatus = statusT.NomeStatus,

            }).ToList();

            return statusDTO;
        }


        public ListarStatusTransferenciaDTO BuscarPorID(Guid statusID)
        {
            StatusTransferencia statusT = _repository.BuscarPorID(statusID);

            if (statusT == null)
            {
                throw new DomainException("Status de trasnferência não encontrado");
            }

            ListarStatusTransferenciaDTO statusDTO = new ListarStatusTransferenciaDTO
            {
                StatusTransferenciaID = statusT.StatusTransferenciaID,
                NomeStatus = statusT.NomeStatus,
            };
            return statusDTO;
        }


        public void Adicionar(CriarStatusTransferenciaDTO criarDTO)
        {
            Validar.ValidarNome(criarDTO.NomeStatus);

            StatusTransferencia statusExiste = _repository.BuscarPorNome(criarDTO.NomeStatus);

            if (statusExiste != null)
            {
                throw new DomainException("Esse status de transferencia já está cadastrado");
            }

            StatusTransferencia statusT = new StatusTransferencia
            {
                NomeStatus = criarDTO.NomeStatus
            };

            _repository.Adicionar(statusT);
        }



        public void Atualizar(Guid id, CriarStatusTransferenciaDTO attDTO)
        {
            Validar.ValidarNome(attDTO.NomeStatus);

            StatusTransferencia statusBanco = _repository.BuscarPorID(id);

            if (statusBanco == null)
            {
                throw new DomainException("Status de transferencia não encontrado");
            }

            StatusTransferencia statusExiste = _repository.BuscarPorNome(attDTO.NomeStatus);

            if (statusExiste != null)
            {
                throw new DomainException("Esse status de transferencia já está cadastrado");
            }

            statusBanco.NomeStatus = attDTO.NomeStatus;

            _repository.Atualizar(statusBanco);
        }
    }
}
