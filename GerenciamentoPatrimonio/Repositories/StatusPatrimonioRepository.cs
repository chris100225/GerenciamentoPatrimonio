using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repositories
{
    public class StatusPatrimonioRepository : IStatusPatrimonioRepository
    {
        private readonly GerenciamentoPatrimonioContext _context;

        public StatusPatrimonioRepository(GerenciamentoPatrimonioContext context)
        {
            _context = context;
        }

        public List<StatusPatrimonio> Listar()
        {
            return _context.StatusPatrimonio.OrderBy(statusP => statusP.NomeStatus).ToList();
        }

        public StatusPatrimonio BuscarPorID(Guid statusID)
        {
            return _context.StatusPatrimonio.Find(statusID);
        }

        public StatusPatrimonio BuscarPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(statusP => statusP.NomeStatus.ToLower() == nomeStatus.ToLower());
        }


        public void Adicionar(StatusPatrimonio statusPatrimonio)
        {
            _context.StatusPatrimonio.Add(statusPatrimonio);
            _context.SaveChanges();
        }



        public void Atualizar(StatusPatrimonio statusP)
        {
            if (statusP == null)
            {
                return;
            }

            StatusPatrimonio statusBanco = _context.StatusPatrimonio.Find(statusP.StatusPatrimonioID);
            if (statusP == null)
            {
                return;
            }

            statusBanco.NomeStatus = statusP.NomeStatus;
            _context.SaveChanges();
        }
    }
}
