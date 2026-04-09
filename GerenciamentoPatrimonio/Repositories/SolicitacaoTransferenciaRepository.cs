using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repositories
{
    public class SolicitacaoTransferenciaRepository : ISolicitacaoTransferenciaRepository
    {

        private readonly GerenciamentoPatrimonioContext _context;

        public SolicitacaoTransferenciaRepository(GerenciamentoPatrimonioContext context)
        {
            _context = context;
        }

        public List<SolicitacaoTransferencia> Listar()
        {
            return _context.SolicitacaoTransferencia.OrderByDescending(solicitacao => solicitacao.DataCriacaoSolicitacao).ToList();
        }
        public SolicitacaoTransferencia BuscarPorID(Guid transferenciaID)
        {
            return _context.SolicitacaoTransferencia.Find(transferenciaID);
        }

        public StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus)
        {
            return _context.StatusTransferencia.FirstOrDefault(status => status.NomeStatus.ToLower() == nomeStatus.ToLower());
        }


        public bool ExisteSolicitacaoPendente(Guid patrimonioID)
        {
            StatusTransferencia statusPendente = BuscarStatusTransferenciaPorNome("Pendente de aprovação");

            if (statusPendente == null)
            {
                return false;
            }

            return _context.SolicitacaoTransferencia.Any(solicitacao => solicitacao.PatrimonioID == patrimonioID && solicitacao.StatusTransferenciaID == statusPendente.StatusTransferenciaID);
        }

        public bool UsuarioResponsavelDaLocalizacao(Guid usuarioID, Guid localizacaoID)
        {
            return _context.Usuario.Any(usuario => usuario.UsuarioID == usuarioID && usuario.Localizacao.Any(localizacao => localizacao.LocalizacaoID == localizacaoID));
        }


        public void Adicionar(SolicitacaoTransferencia solicitacaoTransferencia)
        {
            _context.SolicitacaoTransferencia.Add(solicitacaoTransferencia);
            _context.SaveChanges();
        }

        public bool LocalizacaoExiste(Guid localizacaoID)
        {
            return _context.Localizacao.Any(localizacao => localizacao.LocalizacaoID == localizacaoID);
        }

        public Patrimonio BuscarPatrimonioPorID(Guid patrimonioID)
        {
            return _context.Patrimonio.Find(patrimonioID);
        }

        public StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(status => status.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

        public TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo)
        {
            return _context.TipoAlteracao.FirstOrDefault(tipo => tipo.NomeTipo.ToLower() == nomeTipo.ToLower());
        }

        public void Atualizar(SolicitacaoTransferencia solicitacaoTransferencia)
        {
            if (solicitacaoTransferencia == null) { return; }

            SolicitacaoTransferencia solicitacaoBanco = _context.SolicitacaoTransferencia.Find(solicitacaoTransferencia.SolicitacaoTransferenciaID);

            if (solicitacaoBanco == null) { return; }

            solicitacaoBanco.DataResposta = solicitacaoTransferencia.DataResposta;
            solicitacaoBanco.StatusTransferenciaID = solicitacaoTransferencia.SolicitacaoTransferenciaID;
            solicitacaoBanco.UsuarioIDAprovacao = solicitacaoTransferencia.UsuarioIDAprovacao;

            _context.SaveChanges();
        }

        public void AtualizarPatrimonio(Patrimonio patrimonio)
        {
            if (patrimonio == null) { return; }

            Patrimonio patrimonioBanco = _context.Patrimonio.Find(patrimonio.PatrimonioID);

            if (patrimonioBanco == null) { return; }

            patrimonioBanco.LocalizacaoID = patrimonio.LocalizacaoID;
            patrimonioBanco.StatusPatrimonioID = patrimonio.StatusPatrimonioID;

            _context.SaveChanges();
        }

        public void AdicionarLog(LogPatrimonio logPatrimonio)
        {
            _context.LogPatrimonio.Add(logPatrimonio);
            _context.SaveChanges();
        }
    }
}