using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface ITipoAlteracaoRepository
    {
        List<TipoAlteracao> Listar();
        TipoAlteracao BuscarPorID(Guid tipoAlteracaoID);
        TipoAlteracao BuscarPorNome(string nomeTipo);
        void Adicionar(TipoAlteracao tipoAlteracao);
        void Atualizar(TipoAlteracao tipoAlteracao);
    }
}
