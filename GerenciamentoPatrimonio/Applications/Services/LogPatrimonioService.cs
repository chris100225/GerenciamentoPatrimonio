using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.LogPatrimonioDTO;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class LogPatrimonioService
    {
        private readonly ILogPatrimonioRepository _repository;

        public LogPatrimonioService(ILogPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarLogPatrimonioDTO> Listar()
        {
            List<LogPatrimonio> logs = _repository.Listar();


            List<ListarLogPatrimonioDTO> logsDTO = logs.Select(log => new ListarLogPatrimonioDTO
            {
                LogPatrimonioID = log.LogPatrimonioID,
                DataTransferencia = log.DataTransferencia,
                PatrimonioID = log.PatrimonioID,
                DenominacaoPatrimonio = log.Patrimonio.Denominacao,
                TipoAlteracao = log.TipoAlteracao.NomeTipo,
                StatusPatrimonio = log.StatusPatrimonio.NomeStatus,
                Usuario = log.Usuario.Nome,
                Localizacao = log.Localizacao.NomeLocal,
            }).ToList();

            return logsDTO;
        }

        public List<ListarLogPatrimonioDTO> BuscarPorPatrimonio(Guid patrimonioID)
        {
            List<LogPatrimonio> logs = _repository.BuscarPorPatrimonio(patrimonioID);

            if (logs == null) { throw new DomainException("Patrimonio nao encontrado"); }

            List<ListarLogPatrimonioDTO> logsDTO = logs.Select(log => new ListarLogPatrimonioDTO
            {
                LogPatrimonioID = log.LogPatrimonioID,
                DataTransferencia = log.DataTransferencia,
                PatrimonioID = log.PatrimonioID,
                DenominacaoPatrimonio = log.Patrimonio.Denominacao,
                TipoAlteracao = log.TipoAlteracao.NomeTipo,
                StatusPatrimonio = log.StatusPatrimonio.NomeStatus,
                Usuario = log.Usuario.Nome,
                Localizacao = log.Localizacao.NomeLocal,

            }).ToList();

            return logsDTO;
        }
    }
}
