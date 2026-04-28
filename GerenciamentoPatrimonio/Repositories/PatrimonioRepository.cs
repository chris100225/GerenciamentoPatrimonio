using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repositories
{
    public class PatrimonioRepository : IPatrimonioRepository
    {
        private readonly GerenciamentoPatrimonioContext _context;

        public PatrimonioRepository(GerenciamentoPatrimonioContext context)
        {
            _context = context;
        }

        public List<Patrimonio> Listar()
        {
            return _context.Patrimonio.OrderBy(patrimonio => patrimonio.Denominacao).ToList();
        }

        public Patrimonio BuscarPorID(Guid patrimonioID)
        {
            return _context.Patrimonio.Find(patrimonioID);
        }

        public bool BuscarPorNumeroPatrimonio(string numeroPatrimonio)
        {
            return _context.Patrimonio.Any(patrimonio => patrimonio.NumeroPatrimonio == numeroPatrimonio);

        }

        public bool LocalizacaoExiste(Guid localizacaoID)
        {
            return _context.Localizacao.Any(local => local.LocalizacaoID == localizacaoID);
        }

        public bool StatusPatrimonioExiste(Guid statusPatrimonioID)
        {
            return _context.StatusPatrimonio.Any(statusP => statusP.StatusPatrimonioID == statusPatrimonioID);
        }

        public Localizacao BuscarLocalizacaoPorNome(string nomeLocalizacao)
        {
            return _context.Localizacao.FirstOrDefault(localizacao => localizacao.NomeLocal.ToLower() == nomeLocalizacao.ToLower());
        }

        public StatusPatrimonio BuscarStatusPatrimonio(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(status => status.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

        public TipoAlteracao BuscarTipoAlteracao(string nomeTipo)
        {
            return _context.TipoAlteracao.FirstOrDefault(tipo => tipo.NomeTipo.ToLower() == nomeTipo.ToLower());
        }

        public void Adicionar(Patrimonio patrimonio)
        {
            _context.Patrimonio.Add(patrimonio);
            _context.SaveChanges();
        }

        public void AtualizarStatus(Patrimonio patrimonio)
        {
            if (patrimonio == null)
            {
                return;
            }

            Patrimonio patrimonioBanco = _context.Patrimonio.Find(patrimonio.PatrimonioID);

            if (patrimonioBanco == null)
            {
                return;
            }

            patrimonioBanco.StatusPatrimonioID = patrimonio.StatusPatrimonioID;

            _context.SaveChanges();
        }

        public void AdicionarLog(LogPatrimonio logPatrimonio)
        {
            _context.LogPatrimonio.Add(logPatrimonio);
            _context.SaveChanges();
        }


        public void Atualizar(Patrimonio patrimonio)
        {
            if (patrimonio == null) { return; }

            Patrimonio patrimonioBanco = _context.Patrimonio.Find(patrimonio.PatrimonioID);

            if (patrimonioBanco == null) { return; }

            patrimonioBanco.Denominacao = patrimonio.Denominacao;
            patrimonioBanco.NumeroPatrimonio = patrimonio.NumeroPatrimonio;
            patrimonioBanco.Valor = patrimonio.Valor;
            patrimonioBanco.Imagem = patrimonio.Imagem;
            patrimonioBanco.LocalizacaoID = patrimonio.LocalizacaoID;
            patrimonioBanco.TipoPatrimonioID = patrimonio.TipoPatrimonioID;
            patrimonioBanco.StatusPatrimonioID = patrimonio.StatusPatrimonioID;
            _context.SaveChanges();
        }


    }
}
