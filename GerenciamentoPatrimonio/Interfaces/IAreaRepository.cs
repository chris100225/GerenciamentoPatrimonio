using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface IAreaRepository
    {
        List<Area> Listar();

        Area BuscarPorID(Guid id);
        Area BuscarPorNome(string nomeArea);
        
        void Adicionar(Area area);

        void Atualizar(Area area);
    }
}
