using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface IBairroRepository
    {
        List<Bairro> Listar();
        Bairro BuscarPorID(Guid bairroID);

        void Adicionar(Bairro bairro);
        void Atualizar(Bairro bairro);

        Bairro BuscarPorNome(string nomeBairro, Guid cidadeID);

        bool CidadeExiste(Guid cidadeID);
    }
}
