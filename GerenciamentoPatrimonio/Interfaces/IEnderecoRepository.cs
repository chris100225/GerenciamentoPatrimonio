using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface IEnderecoRepository
    {
        List<Endereco> Listar();
        Endereco BuscarPorID(Guid enderecoId);
        void Adicionar(Endereco endereco);
        //void Atualizar(Endereco endereco);

        Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId);
        bool BairroExiste(Guid bairroId);
    }
}
