using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repositories
{
    public class CargoRepository : ICargoRepository
    {

        private readonly GerenciamentoPatrimonioContext _context;

        public CargoRepository(GerenciamentoPatrimonioContext context)
        {
            _context = context;
        }

        public List<Cargo> Listar()
        {
            return _context.Cargo.OrderBy(cargo => cargo.NomeCargo).ToList();
        }

        public Cargo BuscarPorID(Guid cargoID)
        {
            return _context.Cargo.Find(cargoID);
        }

        public Cargo BuscarPorNome(string nomeCargo)
        {
            return _context.Cargo.FirstOrDefault(cargo => cargo.NomeCargo.ToLower() == nomeCargo.ToLower());
        }


        public void Adicionar(Cargo cargo)
        {
            _context.Cargo.Add(cargo);
            _context.SaveChanges();
        }


        public void Atualizar(Cargo cargo)
        {
            if (cargo == null)
            {
                return;
            }

            Cargo cargoBanco = _context.Cargo.Find(cargo.CargoID);

            if (cargo == null)
            {
                return;
            }

            cargoBanco.NomeCargo = cargo.NomeCargo;
            _context.SaveChanges();
        }
    }
}
