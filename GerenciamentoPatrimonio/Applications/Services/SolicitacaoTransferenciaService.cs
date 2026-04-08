using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.SolicitacaoTransferenciaDTO;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class SolicitacaoTransferenciaService
    {
        private readonly ISolicitacaoTransferenciaRepository _repository;

        private readonly IUsuarioRepository _usuarioRepository;

        public SolicitacaoTransferenciaService(ISolicitacaoTransferenciaRepository repository, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        public List<ListarSolicitacaoTransferenciaDTO> Listar()
        {
            List<SolicitacaoTransferencia> solicitacoes = _repository.Listar();
            List<ListarSolicitacaoTransferenciaDTO> solicitacoesDTO = solicitacoes.Select(solicitacao=>new ListarSolicitacaoTransferenciaDTO
            {
                TransferenciaID = solicitacao.SolicitacaoTransferenciaID,
                DataCriacaoSolicitante = solicitacao.DataCriacaoSolicitacao,
                DataResposta = solicitacao.DataResposta,
                Justificativa = solicitacao.Justificativa,  
                StatusTransferenciaID = solicitacao.StatusTransferenciaID,
                UsuarioIDSolicitacao = solicitacao.UsuarioIDSolicitacao,
                UsuarioIDAprovacao = solicitacao.UsuarioIDAprovacao,
                PatrimonioID = solicitacao.PatrimonioID,
                LocalizacaoID = solicitacao.LocalID
            }).ToList();

            return solicitacoesDTO;
        }

        public ListarSolicitacaoTransferenciaDTO BuscarPorID(Guid transferenciaID)
        {
            SolicitacaoTransferencia solicitacao = _repository.BuscarPorID(transferenciaID);

            if (solicitacao == null)
            {
                throw new DomainException("Solicitação de transferencia não encontrada");
            }

            ListarSolicitacaoTransferenciaDTO solicitacaoDTO = new ListarSolicitacaoTransferenciaDTO
            {
                TransferenciaID = solicitacao.SolicitacaoTransferenciaID,
                DataCriacaoSolicitante = solicitacao.DataCriacaoSolicitacao,
                DataResposta = solicitacao.DataResposta,
                Justificativa = solicitacao.Justificativa,
                StatusTransferenciaID = solicitacao.StatusTransferenciaID,
                UsuarioIDSolicitacao = solicitacao.UsuarioIDSolicitacao,
                UsuarioIDAprovacao = solicitacao.UsuarioIDAprovacao,
                PatrimonioID = solicitacao.PatrimonioID,
                LocalizacaoID = solicitacao.LocalID
            };

            return solicitacaoDTO;
        }
    }
}
