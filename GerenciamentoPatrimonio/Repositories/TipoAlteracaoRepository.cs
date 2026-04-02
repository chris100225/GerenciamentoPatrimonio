using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repositories
{
    public class TipoAlteracaoRepository : ITipoAlteracaoRepository
    {
        private readonly GerenciamentoPatrimonioContext _context;

        public TipoAlteracaoRepository(GerenciamentoPatrimonioContext context)
        {
            _context = context;
        }

        public List<TipoAlteracao> Listar()
        {
            return _context.TipoAlteracao.OrderBy(tipo => tipo.NomeTipo).ToList();
        }

        public TipoAlteracao BuscarPorID(Guid tipoAlteracaoID)
        {
            return _context.TipoAlteracao.Find(tipoAlteracaoID);
        }

        public TipoAlteracao BuscarPorNome(string nomeTipo)
        {
            return _context.TipoAlteracao.FirstOrDefault(TipoAlteracao => TipoAlteracao.NomeTipo.ToLower() == nomeTipo.ToLower());
        }

        public void Adicionar(TipoAlteracao tipoAlteracao)
        {
            _context.TipoAlteracao.Add(tipoAlteracao);
            _context.SaveChanges();
        }

        public void Atualizar(TipoAlteracao tipo)
        {
            if (tipo == null)
            {
                return;
            }

            TipoAlteracao tipoBanco = _context.TipoAlteracao.Find(tipo.TipoAlteracaoID);
            if (tipo == null)
            {
                return;
            }

            tipoBanco.NomeTipo = tipo.NomeTipo;
            _context.SaveChanges();
        }
    }
}
