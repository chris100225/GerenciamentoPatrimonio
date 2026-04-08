using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPatrimonio.Repositories
{
    public class LogPatrimonioRepository : ILogPatrimonioRepository
    {

        private readonly GerenciamentoPatrimonioContext _context;

        public LogPatrimonioRepository(GerenciamentoPatrimonioContext context)
        {
            _context = context;
        }
        public List<LogPatrimonio> Listar()
        {
            return _context.LogPatrimonio
                .Include(log => log.Usuario)
                .Include(log => log.Localizacao)
                .Include(log => log.TipoAlteracao)
                .Include(log => log.StatusPatrimonio)
                .Include(log => log.Patrimonio)
                .Include(log => log.DataTransferencia).ToList();
        }

        public List<LogPatrimonio> BuscarPorPatrimonio(Guid patrimonioID)
        {
            return _context.LogPatrimonio
                .Include(log => log.Usuario)
                .Include(log => log.Localizacao)
                .Include(log => log.TipoAlteracao)
                .Include(log => log.StatusPatrimonio)
                .Include(log => log.Patrimonio)
                .Where(log => log.PatrimonioID == patrimonioID).OrderByDescending(log => log.DataTransferencia).ToList();
        }
    }
}
