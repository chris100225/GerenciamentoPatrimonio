using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPatrimonio.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly GerenciamentoPatrimonioContext _context;

        public UsuarioRepository(GerenciamentoPatrimonioContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.OrderBy(usuario => usuario.Nome).ToList();
        }

        public Usuario BuscarPorID(Guid usuarioID)
        {
            return _context.Usuario.Find(usuarioID);
        }

        public Usuario BuscarDuplicado(string nif, string cpf, string email, Guid? usuarioID)
        {
            var consulta = _context.Usuario.AsQueryable();

            if (usuarioID.HasValue)
            {
                consulta = consulta.Where(usuario => usuario.UsuarioID != usuarioID.Value);
            }

            return consulta.FirstOrDefault(usuario =>
            usuario.NIF == nif ||
            usuario.CPF == cpf ||
            usuario.Email.ToLower() == email.ToLower());
        }

        public bool EnderecoExiste(Guid enderecoID)
        {
            return _context.Endereco.Any(endereco => endereco.EnderecoID == enderecoID);
        }

        public bool CargoExiste(Guid cargoID)
        {
            return _context.Cargo.Any(cargo => cargo.CargoID == cargoID);
        }

        public bool TipoUsuarioExiste(Guid tipoUsuarioID)
        {
            return _context.TipoUsuario.Any(tipoUsuario => tipoUsuario.TipoUsuarioID == tipoUsuarioID);
        }

        public void Adicionar(Usuario usuario)
        {
            if (usuario == null)
            {
                _context.Usuario.Add(usuario);
                _context.SaveChanges();
            }
        }

        public void Atualizar(Usuario usuario)
        {
            if (usuario == null)
            {
                return;
            }

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if (usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.NIF = usuario.NIF;
            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.CPF = usuario.CPF;
            usuarioBanco.RG = usuario.RG;
            usuarioBanco.CarteiraTrabalho = usuario.CarteiraTrabalho;
            usuarioBanco.Email = usuario.Email;
            usuarioBanco.EnderecoID = usuario.EnderecoID;
            usuarioBanco.CargoID = usuario.CargoID;
            usuarioBanco.TipoUsuarioID = usuario.TipoUsuarioID;

            _context.SaveChanges();
        }

        public void AtualizarStatus(Usuario usuario)
        {
            if (usuario == null) { return; }


            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if (usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.Ativo = usuario.Ativo;
            _context.SaveChanges();
        }


        public Usuario ObterPorNIFComTipoUser(string nif)
        {
            return _context.Usuario.Include(usuario => usuario.TipoUsuario)
                .FirstOrDefault(usuario => usuario.NIF == nif);
        }

        public void AtualizarSenha(Usuario usuario)
        {
            if (usuario == null) { return; }

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if (usuarioBanco == null) { return; }

            usuarioBanco.Senha = usuario.Senha;
            _context.SaveChanges();

        }


        public void AtualizarPrimeiroAcesso(Usuario usuario)
        {
            if (usuario == null) { return; }

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if (usuarioBanco == null) { return; }

            usuarioBanco.PrimeiroAcesso = usuario.PrimeiroAcesso;
            _context.SaveChanges();
            {

            }
        }
    }
}