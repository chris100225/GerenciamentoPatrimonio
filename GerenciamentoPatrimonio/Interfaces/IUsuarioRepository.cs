using GerenciamentoPatrimonio.Domains;
using Microsoft.AspNetCore.Components.Web;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();
        Usuario BuscarPorID(Guid usuarioID);

        Usuario BuscarDuplicado(string nif, string cpf, string email, Guid? usuarioID = null);

        bool EnderecoExiste(Guid enderecoID);

        bool CargoExiste(Guid cargoID);

        bool TipoUsuarioExiste(Guid tipoUsuarioID);

        void Adicionar(Usuario usuario);

        void Atualizar(Usuario usuario);

        void AtualzarStatus(Usuario usuario);

        Usuario ObterPorNIFComTipoUser(string nif);
    
        void AtualizarSenha(Usuario usuario);

        void AtualizarPrimeiroAcesso(Usuario usuario);
    }
}
