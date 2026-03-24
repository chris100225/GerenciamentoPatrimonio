using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly GerenciamentoPatrimonioContext _context;

        public AreaRepository(GerenciamentoPatrimonioContext context)
        {
            _context = context;
        }

        public List<Area> Listar()
        {
            return _context.Area.OrderBy(area=>area.NomeArea).ToList();
        }
    }
}
