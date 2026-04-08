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

        public Patrimonio BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioID = null)
        {
            var consulta = _context.Patrimonio.AsQueryable();

            if (patrimonioID.HasValue)
            {
                consulta = consulta.Where(patrimonio => patrimonio.PatrimonioID != patrimonioID.Value);
            }
            return consulta.FirstOrDefault(patrimonio =>
            patrimonio.NumeroPatrimonio.ToLower() == numeroPatrimonio.ToLower() &&
            patrimonio.PatrimonioID == patrimonioID);

        }

        public bool LocalizacaoExiste(Guid localizacaoID)
        {
            return _context.Localizacao.Any(local => local.LocalizacaoID == localizacaoID);
        }

        public bool StatusPatrimonioExiste(Guid statusPatrimonioID)
        {
            return _context.StatusPatrimonio.Any(statusP => statusP.StatusPatrimonioID == statusPatrimonioID);
        }

        public void Adicionar(Patrimonio patrimonio)
        {
            _context.Patrimonio.Add(patrimonio);
            _context.SaveChanges();
        }


        public void Atualizar(Patrimonio patrimonio)
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

            patrimonioBanco.Denominacao = patrimonio.Denominacao;
            patrimonioBanco.NumeroPatrimonio = patrimonio.NumeroPatrimonio;
            patrimonioBanco.Valor = patrimonio.Valor;
            patrimonioBanco.Imagem = patrimonio.Imagem;
            patrimonioBanco.LocalizacaoID = patrimonio.LocalizacaoID;
            patrimonioBanco.TipoPatrimonioID = patrimonio.TipoPatrimonioID;
            patrimonioBanco.StatusPatrimonioID = patrimonio.StatusPatrimonioID;
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
    }
}
