using GerenciamentoPatrimonio.Applications.Regras;
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
            List<ListarSolicitacaoTransferenciaDTO> solicitacoesDTO = solicitacoes.Select(solicitacao => new ListarSolicitacaoTransferenciaDTO
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

        public void Adicionar(Guid usuarioID, CriarSolicitacaoTransferenciaDTO criarDTO)
        {
            Validar.ValidarJustificativa(criarDTO.Justificativa);

            Usuario usuario = _usuarioRepository.BuscarPorID(usuarioID);




            if (usuario == null) { throw new DomainException("Usúário não encontrado"); }




            Patrimonio patrimonio = _repository.BuscarPatrimonioPorID(criarDTO.PatrimonioID);




            if (patrimonio == null) { throw new DomainException("Patrimonio não encontrado"); }


            if (!_repository.LocalizacaoExiste(criarDTO.LocalizacaoID)) { throw new DomainException("Localização de destino não encontrados"); }


            if (patrimonio.LocalizacaoID == criarDTO.LocalizacaoID) { throw new DomainException("O patrimonio já existe nessa localização"); }


            if (_repository.ExisteSolicitacaoPendente(criarDTO.PatrimonioID)) { throw new DomainException("Já existe uma solicitação pendente para esse patimonio"); }


            if (usuario.TipoUsuario.NomeTipo == "Responsável")
            {
                bool usuarioResponsavel = _repository.UsuarioResponsavelDaLocalizacao(usuarioID, patrimonio.LocalizacaoID);

                if (!usuarioResponsavel) { throw new DomainException("O responsável pode solicitar transferencia de patrimonio do ambiente ao qual está vinculado"); }

            }

            StatusTransferencia statusPendente = _repository.BuscarStatusTransferenciaPorNome("Pendente de aprovação");

            if (statusPendente == null) { throw new DomainException("Status de transferncia pendente não encontrado"); }

            SolicitacaoTransferencia solicitacao = new SolicitacaoTransferencia
            {
                DataCriacaoSolicitacao = DateTime.Now,
                Justificativa = criarDTO.Justificativa,
                StatusTransferenciaID = statusPendente.StatusTransferenciaID,
                UsuarioIDSolicitacao = usuarioID,
                UsuarioIDAprovacao = null,
                PatrimonioID = criarDTO.PatrimonioID,
                LocalID = criarDTO.LocalizacaoID
            };

            _repository.Adicionar(solicitacao);
        }

        public void Responder(Guid transferenciaID, Guid usuarioID, ResponderSolicitacaoTransferenciaDTO DTO)
        {
            Usuario usuario = _usuarioRepository.BuscarPorID(usuarioID);



            if (usuario == null) { throw new DomainException("Usuário não encontrado"); }



            SolicitacaoTransferencia solicitacao = _repository.BuscarPorID(transferenciaID);



            if (solicitacao == null) { throw new DomainException("Solicitação de transferencia não encontrada"); }



            Patrimonio patrimonio = _repository.BuscarPatrimonioPorID(solicitacao.PatrimonioID);



            if (patrimonio == null) { throw new DomainException("Patrimonio não encontrado"); }



            StatusTransferencia statusPendente = _repository.BuscarStatusTransferenciaPorNome("Pendente de aprovação");



            if (statusPendente == null) { throw new DomainException("Status pendente não encontrado"); }



            if (solicitacao.StatusTransferenciaID != statusPendente.StatusTransferenciaID) { throw new DomainException("Essa solicitação já foi respondida"); }



            if (usuario.TipoUsuario.NomeTipo == "Responsável")
            {
                bool usuarioResponsavel = _repository.UsuarioResponsavelDaLocalizacao(usuarioID, patrimonio.LocalizacaoID);
                if (usuarioResponsavel) { throw new DomainException("Somente o responsável do ambiente de origem pode aprovar essa solicitação"); }


                StatusTransferencia statusResposta;


                if (DTO.Aprovado) { statusResposta = _repository.BuscarStatusTransferenciaPorNome("Aprovado"); }
                else { statusResposta = _repository.BuscarStatusTransferenciaPorNome("Recusado"); }

                if (statusResposta == null) { throw new DomainException("Status resposta da transferencia não encontrado"); }

                solicitacao.StatusTransferenciaID = statusResposta.StatusTransferenciaID;
                solicitacao.UsuarioIDAprovacao = usuarioID;
                solicitacao.DataResposta = DateTime.Now;

                _repository.Atualizar(solicitacao);

                if (DTO.Aprovado)
                {
                    StatusPatrimonio statusTransferido = _repository.BuscarPorPatrimonioNome("Transferido");

                    if (statusTransferido == null) { throw new DomainException("Status de patrimonio 'Transferido' não encontrado"); }

                    TipoAlteracao tipoAlteracao = _repository.BuscarTipoAlteracaoPorNome("Transferência");

                    if (tipoAlteracao == null) { throw new DomainException("Tipo alteração 'Transferência' não encontrado"); }

                    patrimonio.LocalizacaoID = solicitacao.LocalID;
                    patrimonio.StatusPatrimonioID = statusTransferido.StatusPatrimonioID;

                    _repository.AtualizarPatrimonio(patrimonio);

                    LogPatrimonio log = new LogPatrimonio
                    {
                        DataTransferencia = DateTime.Now,
                        TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                        StatusPatrimonioID = statusTransferido.StatusPatrimonioID,
                        PatrimonioID = patrimonio.PatrimonioID,
                        UsuarioID = usuarioID,
                        LocalizacaoID = patrimonio.LocalizacaoID
                    };
                    _repository.AdicionarLog(log);
                }
            }
        }
    }
}