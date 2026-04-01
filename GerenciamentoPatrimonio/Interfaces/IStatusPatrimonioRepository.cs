using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface IStatusPatrimonioRepository
    {
        List<StatusPatrimonio> Listar();
        StatusPatrimonio BuscarPorID(Guid statusID);
        StatusPatrimonio BuscarPorNome(string nomeStatus);
        void Adicionar(StatusPatrimonio statusPatrimonio);
        void Atualizar(StatusPatrimonio statusPatrimonio);
    }
}
