using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repositories
{
    public class StatusTransferenciaRepository : IStatusTansferenciaRepository
    {
        private readonly GerenciamentoPatrimonioContext _context;

        public StatusTransferenciaRepository(GerenciamentoPatrimonioContext context)
        {
            _context = context;
        }

        public List<StatusTransferencia> Listar()
        {
            return _context.StatusTransferencia.OrderBy(statusT => statusT.NomeStatus).ToList();
        }

        public StatusTransferencia BuscarPorID(Guid statusID)
        {
            return _context.StatusTransferencia.Find(statusID);
        }

        public StatusTransferencia BuscarPorNome(string nomeStatus)
        {
            return _context.StatusTransferencia.FirstOrDefault(statusT => statusT.NomeStatus.ToLower() == nomeStatus.ToLower());
        }


        public void Adicionar(StatusTransferencia statusTransferencia)
        {
            _context.StatusTransferencia.Add(statusTransferencia);
            _context.SaveChanges();
        }


        public void Atualizar(StatusTransferencia statusT)
        {
            if (statusT == null)
            {
                return;
            }

            StatusTransferencia statusBanco = _context.StatusTransferencia.Find(statusT.StatusTransferenciaID);
            if (statusT == null)
            {
                return;
            }

            statusBanco.NomeStatus = statusT.NomeStatus;
            _context.SaveChanges();
        }
    }
}
