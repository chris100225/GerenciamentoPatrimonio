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
            return _context.Area.OrderBy(area => area.NomeArea).ToList();
        }

        public Area BuscarPorID(Guid areaID)
        {
            return _context.Area.Find(areaID);
        }

        public Area BuscarPorNome(string nomeArea)
        {
            return _context.Area.FirstOrDefault(area => area.NomeArea.ToLower() == nomeArea.ToLower());
        }

        public void Adicionar(Area area)
        {
            _context.Area.Add(area);
            _context.SaveChanges();
        }

        public void Atualizar(Area area)
        {
            if (area == null)
            {
                return;
            }

            Area areaBanco = _context.Area.Find(area.AreaID);
            if (area == null)
            {
                return;
            }
            areaBanco.NomeArea = area.NomeArea;
            _context.SaveChanges();
        }
    }
}
