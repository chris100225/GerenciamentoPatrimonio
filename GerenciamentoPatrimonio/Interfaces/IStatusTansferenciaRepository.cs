using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface IStatusTansferenciaRepository
    {
        List<StatusTransferencia> Listar();
        StatusTransferencia BuscarPorID(Guid statusTransferenciaID);
        StatusTransferencia BuscarPorNome(string nomeStatus);
        void Adicionar(StatusTransferencia statusTransferencia);
        void Atualizar(StatusTransferencia statusTransferencia);
    }
}
