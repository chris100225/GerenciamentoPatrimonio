using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repositories
{
    public class TipoPatrimonioRepository : ITipoPatrimonioRepository
    {
        private readonly GerenciamentoPatrimonioContext _context;

        public TipoPatrimonioRepository(GerenciamentoPatrimonioContext context)
        {
            _context = context;
        }

        public List<TipoPatrimonio> Listar()
        {
            return _context.TipoPatrimonio.OrderBy(tipo => tipo.NomeTipo).ToList();
        }

        public TipoPatrimonio BuscarPorID(Guid tipoPatrimonioID)
        {
            return _context.TipoPatrimonio.Find(tipoPatrimonioID);
        }

        public TipoPatrimonio BuscarPorNome(string nomeTipo)
        {
            return _context.TipoPatrimonio.FirstOrDefault(TipoPatrimonio => TipoPatrimonio.NomeTipo.ToLower() == nomeTipo.ToLower());
        }

        public void Adicionar(TipoPatrimonio tipoPatrimonio)
        {
            _context.TipoPatrimonio.Add(tipoPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(TipoPatrimonio tipo)
        {
            if (tipo == null)
            {
                return;
            }

            TipoPatrimonio tipoBanco = _context.TipoPatrimonio.Find(tipo.TipoPatrimonioID);
            if (tipo == null)
            {
                return;
            }

            tipoBanco.NomeTipo = tipo.NomeTipo;
            _context.SaveChanges();
        }
    }
}
