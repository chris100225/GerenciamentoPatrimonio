using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface ICargoRepository
    {
        List<Cargo> Listar();
        Cargo BuscarPorID(Guid cargoID);
        Cargo BuscarPorNome(string nomeCargo);
        void Adicionar(Cargo cargo);
        void Atualizar(Cargo cargo);
    }
}
