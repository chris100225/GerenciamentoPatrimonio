using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly GerenciamentoPatrimonioContext _context;

        public TipoUsuarioRepository(GerenciamentoPatrimonioContext context)
        {
            _context = context;
        }

        public List<TipoUsuario> Listar()
        {
            return _context.TipoUsuario.OrderBy(localizacao => localizacao.NomeTipo).ToList();
        }

        public TipoUsuario BuscarPorID(Guid tipoUsuarioID)
        {
            return _context.TipoUsuario.Find(tipoUsuarioID);
        }

        public TipoUsuario BuscarPorNome(string nomeTipo)
        {
            return _context.TipoUsuario.FirstOrDefault(tipoUsuario => tipoUsuario.NomeTipo.ToLower() == nomeTipo.ToLower());
        }

        public void Adicionar(TipoUsuario tipoUsuario)
        {
            _context.TipoUsuario.Add(tipoUsuario);
            _context.SaveChanges();
        }

        public void Atualizar(TipoUsuario tipo)
        {
            if (tipo == null)
            {
                return;
            }

            TipoUsuario tipoBanco = _context.TipoUsuario.Find(tipo.TipoUsuarioID);
            if (tipo == null)
            {
                return;
            }

            tipoBanco.NomeTipo = tipo.NomeTipo;
            _context.SaveChanges();
        }


    }
}
